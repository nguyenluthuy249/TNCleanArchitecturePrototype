using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Xml.Linq;
using TN.CleanArchitectureBlazorApp.Application.Business.Employees.Models;

namespace TN.CleanArchitectureBlazorApp.Pages.Home.Components
{
    public partial class EmployeeAddEditDialogComponent
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public Employee Employee { get; set; }


        MudForm _employeeForm;
        bool _isFormValid;
        List<string> _jobTitleList = new List<string>()
        {
            "Software Developer",
            "Frontend Developer",
            "Backend Developer",
            "Project Manager",
            "UI/UX Designer",
            "Data Analyst",
            "Systems Analyst",
            "QA Engineer",
            "DevOps Engineer",
            "Business Analyst"
        };

        async Task Submit()
        {
            await _employeeForm.Validate();
            if (_isFormValid)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
        void Cancel() => MudDialog.Cancel();
    }
}
