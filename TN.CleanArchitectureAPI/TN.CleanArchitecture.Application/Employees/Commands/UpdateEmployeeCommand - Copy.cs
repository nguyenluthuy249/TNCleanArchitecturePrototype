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
    public class DeleteEmployeeCommand : IRequestWrapper<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandlerWrapper<DeleteEmployeeCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task<ServiceResult<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Employees.FindAsync(request.Id);
            if (entity == null)
            {
                return ServiceResult.Failed<bool>(ServiceError.NotFound);
            }

            _context.Employees.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return ServiceResult.Success(true);
        }
    }
}
