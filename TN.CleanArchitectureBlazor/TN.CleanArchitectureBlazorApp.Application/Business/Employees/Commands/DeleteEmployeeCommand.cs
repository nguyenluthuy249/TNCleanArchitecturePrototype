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
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : BaseCQRSHandler, IRequestHandler<DeleteEmployeeCommand, bool>
    {
        public DeleteEmployeeCommandHandler(ICustomHttpClient client) : base(client)
        {
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await _client.Delete<bool>($"Employee/{request.Id}");
            return result.Data;
        }
    }
}
