using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    public class EmployeeAddViewModel : Screen
    {
        public IEmployeeService service;
        public EmployeeAddViewModel(IEmployeeService service)
        {
            NewEmployee = new Employee();
            this.service = service;
        }

        protected override void OnDeactivate(bool close)
        {
            if (close)
                NewEmployee = null;
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(true);
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

        public async void Save()
        {
            var EmployeeToAdd = NewEmployee;
            TryClose(true);
            await Task.Run(() =>
            {
                service.AddEmployee(EmployeeToAdd);
            });
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
