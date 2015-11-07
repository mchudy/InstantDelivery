using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class EmployeesManagedPackagesViewModel : EmployeesViewModelBase
    {
        private readonly EmployeeService repository;

        public EmployeesManagedPackagesViewModel(EmployeeService repository)
        {
            this.repository = repository;
            Employees = repository.GetAll();
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return repository.GetAll();
        }
    }
}