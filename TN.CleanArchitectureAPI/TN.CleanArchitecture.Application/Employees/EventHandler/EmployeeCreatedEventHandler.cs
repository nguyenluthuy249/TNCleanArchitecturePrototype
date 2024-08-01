using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using TN.Prototype.CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace TN.CleanArchitecture.Application.Employees.EventHandler
{
    public class EmployeeCreatedEventHandler : INotificationHandler<DomainEventNotification<EmployeeCreatedEvent>>
    {
        private readonly ILogger<EmployeeCreatedEventHandler> _logger;
        private readonly IEmailService _emailService;

        public EmployeeCreatedEventHandler(ILogger<EmployeeCreatedEventHandler> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public Task Handle(DomainEventNotification<EmployeeCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var employeedCreated = notification.DomainEvent;
            _logger.LogInformation("Employee {FirstName} {LastName} created with Id {Id} at {Date}.",
                employeedCreated.Employee.FirstName,
                employeedCreated.Employee.LastName,
                employeedCreated.Employee.Id,
                employeedCreated.DateOccurred);

            _emailService.SendEmail(new Email { Subject = "Created employee", Body = $"{employeedCreated.Employee.FirstName} {employeedCreated.Employee.LastName}" });

            return Task.CompletedTask;
        }
    }
}
