using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class EmployeesUsedVehiclesViewModel : EmployeesViewModelBase
    {
        private readonly EmployeeService repository;
        private readonly IWindowManager windowManager;
        private Employee selectedRow;

        public EmployeesUsedVehiclesViewModel(EmployeeService repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Employees = repository.GetAll();
        }

        public Employee SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => IsEnabledViewDetails);
            }
        }

        public bool IsEnabledViewDetails => SelectedRow != null;

        public void ShowVehicleDetails()
        {
            if (SelectedRow == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new EmployeeUsedVehiclesDetailsViewModel
            {
                SelectedRow = SelectedRow
            });
            if (result != true)
            {
                repository.Reload(SelectedRow);
            }
            else
            {
                repository.Save();
            }
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return repository.GetAll();
        }
    }
}