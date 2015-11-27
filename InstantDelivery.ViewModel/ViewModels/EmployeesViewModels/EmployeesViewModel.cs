using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku pracowników.
    /// </summary>
    public class EmployeesViewModel : EmployeesViewModelBase
    {
        private readonly IEmployeeService employeesService;
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
        public EmployeesViewModel(IEmployeeService employeesService, IWindowManager windowManager,
            EmployeeEditViewModel employeeEditViewModel, ConfirmDeleteViewModel confirmDeleteViewModel)
            : base(employeesService)
        {
            this.employeesService = employeesService;
            this.windowManager = windowManager;
            this.employeeEditViewModel = employeeEditViewModel;
            this.confirmDeleteViewModel = confirmDeleteViewModel;
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
                //employeesService.Reload(SelectedEmployee);
            }
            else
            {
                employeesService.Save();
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
                //employeesService.RemoveEmployee(SelectedEmployee);
                UpdateData();
            }
        }
    }
}