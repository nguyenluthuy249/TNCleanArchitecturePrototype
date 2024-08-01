using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Behaviors
{
    public class ApplicationEventDispatchBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IPublisher _publisher;
        public ApplicationEventDispatchBehavior(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();
            if (response is IHasApplicationEvent && ((IHasApplicationEvent)response).ApplicationEvents.Count > 0)
            {
                foreach (var @event in ((IHasApplicationEvent)response).ApplicationEvents)
                {
                    if (!@event.IsPublished)
                    {
                        @event.IsPublished = true;
                        await _publisher.Publish(GetApplicationEventNotification(@event));
                    }
                }

                // After dispatching, clear all events
                ((IHasApplicationEvent)response).ApplicationEvents.Clear();
            }

            return response;
        }

        private INotification GetApplicationEventNotification(ApplicationEvent applicationEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(ApplicationEventNotification<>).MakeGenericType(applicationEvent.GetType()), applicationEvent);
        }
    }
}
