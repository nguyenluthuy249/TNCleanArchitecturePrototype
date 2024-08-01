using FluentValidation;
using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.CleanArchitecture.Application.Employees.Commands
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator(IApplicationDbContext dbContext)
        {
            RuleFor(t => t).Must(t => !dbContext.Employees.Any(e => e.FirstName == t.Employee.FirstName && e.LastName == t.Employee.LastName))
                           .WithMessage("Duplicated employee");

            RuleFor(t => t.Employee.FirstName).MaximumLength(50).WithMessage("FirstName must not exceed 50 characters")
                                     .NotEmpty().WithMessage("FirstName is required.");
            RuleFor(t => t.Employee.LastName).MaximumLength(50).WithMessage("LastName must not exceed 50 characters")
                                     .NotEmpty().WithMessage("LastName is required.");
        }
    }
}
