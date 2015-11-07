using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class EmployeesManagedPackagesViewModel : EmployeesViewModelBase
    {
        private readonly EmployeeService employeesService;

        public EmployeesManagedPackagesViewModel(EmployeeService employeesService)
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