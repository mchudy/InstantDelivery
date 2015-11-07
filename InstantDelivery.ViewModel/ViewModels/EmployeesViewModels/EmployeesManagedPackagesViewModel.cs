using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class EmployeesManagedPackagesViewModel : EmployeesViewModelBase
    {
        private readonly IEmployeeService employeesService;

        public EmployeesManagedPackagesViewModel(IEmployeeService employeesService)
        {
            this.employeesService = employeesService;
            Employees = employeesService.GetAll();
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return employeesService.GetAll();
        }
    }
}