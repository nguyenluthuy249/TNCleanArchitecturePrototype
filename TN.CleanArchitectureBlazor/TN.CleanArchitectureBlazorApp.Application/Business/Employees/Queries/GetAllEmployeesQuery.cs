using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.CleanArchitectureBlazorApp.Application.Business.Employees.Models;
using TN.CleanArchitectureBlazorApp.Application.Framework;
using TN.CleanArchitectureBlazorApp.Application.Models;

namespace TN.CleanArchitectureBlazorApp.Application.Business.Employees.Queries
{
    public class GetAllEmployeesQuery : IRequest<List<Employee>>
    {
    }

    public class GetAllEmployeesQueryHandler : BaseCQRSHandler, IRequestHandler<GetAllEmployeesQuery, List<Employee>>
    {
        public GetAllEmployeesQueryHandler(ICustomHttpClient client) : base(client)
        {
        }

        public async Task<List<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var result = await _client.Get<List<Employee>>($"Employee");
            return result.Data;
        }
    }
}
