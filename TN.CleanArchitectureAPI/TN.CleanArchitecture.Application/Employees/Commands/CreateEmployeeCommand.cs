using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using TN.Prototype.CleanArchitecture.Domain.Entities;
using TN.Prototype.CleanArchitecture.Domain.Events;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TN.CleanArchitecture.Application.Employees.Dtos;
using Mapster;

namespace TN.CleanArchitecture.Application.Employees.Commands
{
    public class CreateEmployeeCommand : IRequestWrapper<EmployeeDto>
    {
        public EmployeeDto Employee { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandlerWrapper<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Employee.Adapt<Employee>();
            entity.DomainEvents.Add(new EmployeeCreatedEvent(entity));
            await _context.Employees.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return ServiceResult.Success(entity.Adapt<EmployeeDto>());
        }
    }
}
