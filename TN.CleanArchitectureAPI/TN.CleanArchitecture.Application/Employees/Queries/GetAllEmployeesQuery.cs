using TN.Prototype.CleanArchitecture.Application.Common.Extensions;
using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Application.Common.Models;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TN.CleanArchitecture.Application.Employees.Dtos;
using Microsoft.EntityFrameworkCore;

namespace TN.CleanArchitecture.Application.Employees.Queries
{
    public class GetAllEmployeesQuery : PagingQuery, IRequestWrapper<List<EmployeeDto>> { }

    public class GetAllEmployeesQueryHandler : IRequestHandlerWrapper<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Employees
                                     .ProjectToType<EmployeeDto>(_mapper.Config)
                                     .ToListAsync();

            return ServiceResult.Success(list);
        }
    }
}
