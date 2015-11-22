using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku pracowników i paczek przez nich dostarczanych.
    /// </summary>
    public class EmployeesManagedPackagesViewModel : EmployeesViewModelBase
    {
        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeesesEmployeesService"></param>
        public EmployeesManagedPackagesViewModel(IEmployeeService employeesesEmployeesService)
            : base(employeesesEmployeesService)
        {
        }

    }
}