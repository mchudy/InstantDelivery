using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using System;

namespace InstantDelivery.ViewModel
{
    public class EmployeeAddViewModel : Screen
    {
        public EmployeeAddViewModel()
        {
            NewEmployee = new Employee();
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
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
