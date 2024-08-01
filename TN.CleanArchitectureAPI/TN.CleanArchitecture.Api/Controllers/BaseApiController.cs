﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace TN.Prototype.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
