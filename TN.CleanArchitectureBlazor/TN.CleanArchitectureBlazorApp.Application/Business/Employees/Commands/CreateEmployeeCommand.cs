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
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public Employee Employee { get; set; }
    }

    public class CreateEmployeeCommandHandler : BaseCQRSHandler, IRequestHandler<CreateEmployeeCommand, Employee>
    {
        public CreateEmployeeCommandHandler(ICustomHttpClient client) : base(client)
        {
        }

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await _client.Post<Employee>($"Employee", request.Employee);
            return result.Data;
        }
    }
}
