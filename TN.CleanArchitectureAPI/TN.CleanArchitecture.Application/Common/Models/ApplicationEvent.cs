using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Models
{
    public abstract class ApplicationEvent
    {
        protected ApplicationEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;

        public bool IsPublished { get; set; }
    }

    public class ApplicationEventNotification<TApplicationEvent> : INotification where TApplicationEvent : ApplicationEvent
    {
        public TApplicationEvent ApplicationEvent { get; }
        public ApplicationEventNotification(TApplicationEvent applicationEvent)
        {
            ApplicationEvent = applicationEvent;
        }
    }
}
