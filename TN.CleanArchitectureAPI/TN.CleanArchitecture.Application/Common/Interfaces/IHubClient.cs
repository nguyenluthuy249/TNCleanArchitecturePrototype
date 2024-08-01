using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Interfaces
{
    public interface IHubClient
    {
        Task Broadcast(object message);
    }
}
