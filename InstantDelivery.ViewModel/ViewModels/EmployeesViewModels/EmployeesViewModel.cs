using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku pracowników.
    /// </summary>
    public class EmployeesViewModel : EmployeesViewModelBase
    {
        private EmployeesServiceProxy proxy;
        private readonly IWindowManager windowManager;
        private EmployeeDto selectedEmployee;
        private EmployeeEditViewModel employeeEditViewModel;
        private ConfirmDeleteViewModel confirmDeleteViewModel;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeesService"></param>
        /// <param name="windowManager"></param>
        /// <param name="employeeEditViewModel"></param>
        /// <param name="confirmDeleteViewModel"></param>
        public EmployeesViewModel(IWindowManager windowManager, EmployeeEditViewModel employeeEditViewModel,
            ConfirmDeleteViewModel confirmDeleteViewModel, EmployeesServiceProxy proxy)
        {
            this.windowManager = windowManager;
            this.employeeEditViewModel = employeeEditViewModel;
            this.confirmDeleteViewModel = confirmDeleteViewModel;
            this.proxy = proxy;
        }

        /// <summary>
        /// Aktualnie zaznaczony wiersz w tabeli danych.
        /// </summary>
        public EmployeeDto SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => IsSelectedAnyRow);
            }
        }

        /// <summary>
        /// Flaga informująca o tym czy aktualnie zaznaczony jest jakiś wiersz w tabeli danych.
        /// </summary>
        public bool IsSelectedAnyRow => SelectedEmployee != null;

        /// <summary>
        /// Delegat zdarzenia przejścia do widoku edycji pracownika.
        /// </summary>
        public async void EditEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            employeeEditViewModel.Employee = SelectedEmployee;
            var result = windowManager.ShowDialog(employeeEditViewModel);
            if (result != true)
            {
                var oldEmployee = await proxy.GetById(selectedEmployee.Id);
                ResetRow(oldEmployee);
            }
            else
            {
                await proxy.UpdateEmployee(SelectedEmployee);
            }
        }

        private void ResetRow(EmployeeDto oldEmployee)
        {
            int index = Employees.IndexOf(SelectedEmployee);
            Employees.Remove(SelectedEmployee);
            Employees.Insert(index, oldEmployee);
            SelectedEmployee = oldEmployee;
        }

        /// <summary>
        /// Delegat zdarzenia usuwania pracownika.
        /// </summary>
        public async void RemoveEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(confirmDeleteViewModel);
            if (result == true)
            {
                await proxy.DeleteEmployee(SelectedEmployee.Id);
                UpdateData();
            }
        }
    }
}