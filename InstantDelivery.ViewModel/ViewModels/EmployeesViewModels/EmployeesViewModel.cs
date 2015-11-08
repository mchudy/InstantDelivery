using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku pracowników.
    /// </summary>
    public class EmployeesViewModel : EmployeesViewModelBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;
        private EmployeeEditViewModel employeeEditViewModel;
        private ConfirmDeleteViewModel confirmDeleteViewModel;
        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="windowManager"></param>
        /// <param name="employeeEditViewModel"></param>
        /// <param name="confirmDeleteViewModel"></param>
        public EmployeesViewModel(IEmployeeService employeeService, IWindowManager windowManager,
            EmployeeEditViewModel employeeEditViewModel, ConfirmDeleteViewModel confirmDeleteViewModel)
        {
            this.employeeService = employeeService;
            this.windowManager = windowManager;
            this.employeeEditViewModel = employeeEditViewModel;
            this.confirmDeleteViewModel = confirmDeleteViewModel;
            Employees = employeeService.GetAll();
        }
        /// <summary>
        /// Aktualnie zaznaczony wiersz w tabeli danych.
        /// </summary>
        public Employee SelectedEmployee
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
        public void EditEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            employeeEditViewModel.Employee = SelectedEmployee;
            var result = windowManager.ShowDialog(employeeEditViewModel);
            if (result != true)
            {
                employeeService.Reload(SelectedEmployee);
            }
            else
            {
                employeeService.Save();
            }
        }
        /// <summary>
        /// Delegat zdarzenia usuwania pracownika.
        /// </summary>
        public void RemoveEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(confirmDeleteViewModel);
            if (result == true)
            {
                employeeService.RemoveEmployee(SelectedEmployee);
                UpdateEmployees();
            }
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return employeeService.GetAll();
        }
    }
}