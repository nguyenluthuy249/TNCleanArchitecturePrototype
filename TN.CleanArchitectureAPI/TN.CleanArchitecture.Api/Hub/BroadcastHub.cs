using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace TN.Prototype.CleanArchitecture.Api.Hub
{
    public class BroadcastHub : Hub<IHubClient> { }
}
