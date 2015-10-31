using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;

namespace InstantDelivery.ViewModel
{
    public class EmployeeAddViewModel : Screen
    {
        private readonly EmployeesRepository repository;
        private readonly IWindowManager windowManager;

        public EmployeeAddViewModel(EmployeesRepository repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            NewEmployee = new Employee();
        }

        protected override void OnDeactivate(bool close)
        {
            NewEmployee = null;
        }

        private Employee newEmployee;
        public Employee NewEmployee
        {
            get { return newEmployee; }
            set
            {
                newEmployee = value;
                NotifyOfPropertyChange();
            }
        }

        public void Save()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
