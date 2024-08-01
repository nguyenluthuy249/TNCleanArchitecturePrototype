using TN.Prototype.CleanArchitecture.Domain.Common;
using TN.Prototype.CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Domain.Events
{
    public class EmployeeCreatedEvent : DomainEvent
    {
        public EmployeeCreatedEvent(Employee employee)
        {
            Employee = employee;
        }

        public Employee Employee { get; }
    }
}
