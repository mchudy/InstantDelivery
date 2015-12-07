using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using System;
using InstantDelivery.Model.Employees;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku dodawania pracownika
    /// </summary>
    public class EmployeeAddViewModel : Screen
    {
        private EmployeesServiceProxy proxy;
        private EmployeeAddDto newEmployee = new EmployeeAddDto();

        public EmployeeAddViewModel(EmployeesServiceProxy proxy)
        {
            this.proxy = proxy;
        }

        /// <summary>
        /// Aktualnie tworzony pracownik.
        /// </summary>
        public EmployeeAddDto NewEmployee
        {
            get { return newEmployee; }
            set
            {
                newEmployee = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Zapisuje zmiany dokonane w widoku.
        /// </summary>
        public async void Save()
        {
            var EmployeeToAdd = NewEmployee;
            TryClose(true);
            await proxy.AddEmployee(EmployeeToAdd);
        }

        /// <summary>
        /// Anuluje zmiany dokonane w widoku.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(true);
        }

        protected override void OnDeactivate(bool close)
        {
            if (close)
                NewEmployee = null;
        }
    }
}
