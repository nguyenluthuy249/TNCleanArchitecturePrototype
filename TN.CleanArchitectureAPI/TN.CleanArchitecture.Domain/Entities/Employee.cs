using TN.Prototype.CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Domain.Entities
{
    public class Employee : AuditableEntity, IHasDomainEvent
    {
        public Employee()
        {
            DomainEvents = new List<DomainEvent>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public decimal HourlyRate { get; set; }
        public List<DomainEvent> DomainEvents { get; set; }

    }
}
