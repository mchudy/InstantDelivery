using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;

namespace InstantDelivery.ViewModel
{
    public class EmployeeDeleteViewModel : Screen
    {
        protected override void OnDeactivate(bool close)
        {
            SelectedEmployee = null;
        }

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                NotifyOfPropertyChange();
            }
        }

        public void Remove()
        {
            if (SelectedEmployee == null)
                return;
            var repository = new EmployeesRepository();
            repository.Remove(SelectedEmployee);
            SelectedEmployee = null;
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
