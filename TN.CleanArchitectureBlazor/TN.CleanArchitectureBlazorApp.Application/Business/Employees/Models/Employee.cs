using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.CleanArchitectureBlazorApp.Application.Business.Employees.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public decimal? HourlyRate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
