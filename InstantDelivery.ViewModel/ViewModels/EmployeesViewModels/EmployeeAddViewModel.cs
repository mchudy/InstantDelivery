using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku dodawania pracownika
    /// </summary>
    public class EmployeeAddViewModel : Screen
    {
        private IEmployeeService service;
        private Employee newEmployee;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="service"></param>
        public EmployeeAddViewModel(IEmployeeService service)
        {
            this.service = service;
            NewEmployee = new Employee();
        }

        /// <summary>
        /// Aktualnie tworzony pracownik.
        /// </summary>
        public Employee NewEmployee
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
            await Task.Run(() =>
            {
                service.AddEmployee(EmployeeToAdd);
            });
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
