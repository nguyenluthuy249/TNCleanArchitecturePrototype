using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TN.CleanArchitecture.Application.Employees.Dtos;

namespace TN.CleanArchitecture.Application.Employees.Queries
{
    public class GetEmployeeByIdQuery : IRequestWrapper<EmployeeDto>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeByIdQueryHandler : IRequestHandlerWrapper<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.Where(t => t.Id == request.Id)
                                           .ProjectToType<EmployeeDto>(_mapper.Config)
                                           .FirstOrDefaultAsync(cancellationToken);

            return employee != null ? ServiceResult.Success(employee) : ServiceResult.Failed<EmployeeDto>(ServiceError.NotFound);
        }
    }

}
