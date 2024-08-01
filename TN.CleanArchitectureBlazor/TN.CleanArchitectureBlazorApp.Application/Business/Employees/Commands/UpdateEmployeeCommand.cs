using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.CleanArchitectureBlazorApp.Application.Business.Employees.Models;
using TN.CleanArchitectureBlazorApp.Application.Business.Employees.Queries;
using TN.CleanArchitectureBlazorApp.Application.Framework;
using TN.CleanArchitectureBlazorApp.Application.Models;

namespace TN.CleanArchitectureBlazorApp.Application.Business.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest<Employee>
    {
        public Employee Employee { get; set; }
    }

    public class UpdateEmployeeCommandHandler : BaseCQRSHandler, IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        public UpdateEmployeeCommandHandler(ICustomHttpClient client) : base(client)
        {
        }

        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await _client.Put<Employee>($"Employee/{request.Employee.Id}", request.Employee);
            return result.Data;
        }
    }
}
