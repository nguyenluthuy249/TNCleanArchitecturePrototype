using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Api.Hub
{
    public class ObjectStateChangedEventHandler : INotificationHandler<ApplicationEventNotification<ObjectStateChangedEvent>>
    {
        private IHubContext<BroadcastHub, IHubClient> _hubContext;
        public ObjectStateChangedEventHandler(IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(ApplicationEventNotification<ObjectStateChangedEvent> notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.Broadcast(notification.ApplicationEvent.Message);
        }
    }
}
