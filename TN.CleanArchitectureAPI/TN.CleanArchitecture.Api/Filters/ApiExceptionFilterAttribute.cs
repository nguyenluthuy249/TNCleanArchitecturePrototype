using TN.Prototype.CleanArchitecture.Application.Common.Exceptions;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
               { typeof(ValidationException), HandleValidationException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }
            HandleUnknownException(context);
        }
        private void HandleValidationException(ExceptionContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                var details = ServiceResult.Failed(exception.Errors, ServiceError.Validation);

                context.Result = new BadRequestObjectResult(details);
            }

            context.ExceptionHandled = true;
        }
        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var exception = new ValidateModelException(context.ModelState);

            context.Result = new BadRequestObjectResult(ServiceResult.Failed(exception.Errors, ServiceError.ValidationFormat));

            context.ExceptionHandled = true;
        }
        private void HandleUnknownException(ExceptionContext context)
        {
            var details = ServiceResult.Failed(new ServiceError(context.Exception.Message, StatusCodes.Status500InternalServerError));
            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}
