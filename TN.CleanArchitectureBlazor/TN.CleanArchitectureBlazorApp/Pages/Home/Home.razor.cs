using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TN.CleanArchitectureBlazorApp.Application.Business.Employees.Commands;
using TN.CleanArchitectureBlazorApp.Application.Business.Employees.Models;
using TN.CleanArchitectureBlazorApp.Application.Business.Employees.Queries;
using TN.CleanArchitectureBlazorApp.Application.Models;
using TN.CleanArchitectureBlazorApp.Pages.Home.Components;
using static MudBlazor.CategoryTypes;

namespace TN.CleanArchitectureBlazorApp.Pages.Home
{
    public partial class Home
    {
        [Inject] IMediator _mediator { get; set; }
        [Inject] IDialogService _dialogService { get; set; }
        [Inject] ISnackbar _snackbar { get; set; }

        List<Employee> _employees;

        protected override async Task OnInitializedAsync()
        {
            _employees = await _mediator.Send(new GetAllEmployeesQuery());
        }

        async Task Add()
        {
            var newEmployee = new Employee();
            var parameters = new DialogParameters<EmployeeAddEditDialogComponent> {
                { x => x.Employee, newEmployee}
            };
            var dialog = _dialogService.Show<EmployeeAddEditDialogComponent>("Add New Employee",
                                         parameters,
                                         new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true });


            var dialogResult = await dialog.Result;
            if (!dialogResult.Canceled)
            {
                try
                {
                    var result = await _mediator.Send(new CreateEmployeeCommand { Employee = newEmployee });
                    _employees.Add(result);
                    _snackbar.Add("Added successfully.", Severity.Success);
                    StateHasChanged();
                }
                catch (Exception)
                {
                    _snackbar.Add("There was an error while adding new employee.", Severity.Error);
                }
            }
        }

        async Task Edit(Employee employee)
        {
            var parameters = new DialogParameters<EmployeeAddEditDialogComponent> {
                { x => x.Employee, employee}
            };
            var dialog = _dialogService.Show<EmployeeAddEditDialogComponent>("Update Employee",
                                         parameters,
                                         new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true });


            var dialogResult = await dialog.Result;
            if (!dialogResult.Canceled)
            {
                try
                {
                    var result = await _mediator.Send(new UpdateEmployeeCommand { Employee = employee });
                    _snackbar.Add("Updated successfully.", Severity.Success);
                    StateHasChanged();
                }
                catch (Exception)
                {
                    _snackbar.Add("There was an error while updating employee.", Severity.Error);
                }
            }
        }

        async Task Delete(Employee employee)
        {
            bool? result = await _dialogService.ShowMessageBox("Employee Delete Confirmation",
                                                                $"You are about to delete employee '{employee.FirstName} {employee.LastName}'.This action cannot be undone. Would you like to proceed?",
                                                                yesText: "Yes", noText: "No");
            if (result.HasValue && result.Value)
            {
                try
                {
                    await _mediator.Send(new DeleteEmployeeCommand { Id = employee .Id});
                    _snackbar.Add("Deleted successfully.", Severity.Success);
                    _employees.Remove(employee);
                    StateHasChanged();
                }
                catch (Exception)
                {
                    _snackbar.Add($"There was an error deleting employee '{employee.FirstName} {employee.LastName}'.", Severity.Error);
                }
            }
        }
    }
}
