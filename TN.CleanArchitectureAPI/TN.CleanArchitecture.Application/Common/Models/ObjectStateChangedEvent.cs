using TN.Prototype.CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Models
{
    public class ObjectStateChangedEvent : ApplicationEvent
    {
        public ObjectStateChangedEvent(object message)
        {
            Message = message;
        }

        public object Message { get; }
    }


}
