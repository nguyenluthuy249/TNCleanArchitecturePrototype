using Mapster;
using MapsterMapper;
using System.Threading;
using System.Threading.Tasks;
using TN.CleanArchitecture.Application.Employees.Dtos;
using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using TN.Prototype.CleanArchitecture.Domain.Entities;
using TN.Prototype.CleanArchitecture.Domain.Events;

namespace TN.CleanArchitecture.Application.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequestWrapper<EmployeeDto>
    {
        public EmployeeDto Employee { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandlerWrapper<UpdateEmployeeCommand, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task<ServiceResult<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Employees.FindAsync(request.Employee.Id);
            if (entity == null)
            {
                return ServiceResult.Failed<EmployeeDto>(ServiceError.NotFound);
            }

            entity.FirstName = request.Employee.FirstName;
            entity.LastName = request.Employee.LastName;
            entity.JobTitle = request.Employee.JobTitle;
            entity.HourlyRate = request.Employee.HourlyRate;
            await _context.SaveChangesAsync(cancellationToken);
            return ServiceResult.Success(entity.Adapt<EmployeeDto>());
        }
    }
}
