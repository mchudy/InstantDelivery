using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using System;
using InstantDelivery.Core.Repositories;

namespace InstantDelivery.ViewModel
{
    public class EmployeeAddViewModel : Screen
    {
        public EmployeeService service;
        public EmployeeAddViewModel(EmployeeService service)
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

        public void Save()
        {
            service.AddEmployee(NewEmployee);
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
