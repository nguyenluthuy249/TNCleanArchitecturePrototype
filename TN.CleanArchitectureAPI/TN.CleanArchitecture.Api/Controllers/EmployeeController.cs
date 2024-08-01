using TN.Prototype.CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TN.CleanArchitecture.Application.Employees.Commands;
using TN.CleanArchitecture.Application.Employees.Dtos;
using TN.CleanArchitecture.Application.Employees.Queries;

namespace TN.Prototype.CleanArchitecture.Api.Controllers
{
    public class EmployeeController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<EmployeeDto>>>> GetAllEmployees([FromQuery] GetAllEmployeesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<List<EmployeeDto>>>> GetEmployeeById(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdQuery() { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<EmployeeDto>>> Create([FromBody] EmployeeDto employee)
        {
            return Ok(await Mediator.Send(new CreateEmployeeCommand {  Employee = employee}));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResult<EmployeeDto>>> Update([FromBody] EmployeeDto employee)
        {
            return Ok(await Mediator.Send(new UpdateEmployeeCommand { Employee = employee }));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<EmployeeDto>>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteEmployeeCommand { Id = id }));
        }
    }
}
