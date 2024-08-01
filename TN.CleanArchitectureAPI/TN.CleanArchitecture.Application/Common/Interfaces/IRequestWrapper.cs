using TN.Prototype.CleanArchitecture.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Interfaces
{
    public interface IRequestWrapper<T> : IRequest<ServiceResult<T>> { }

    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ServiceResult<TOut>> where TIn : IRequestWrapper<TOut> { }
}
