using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku pracowników i paczek przez nich dostarczanych.
    /// </summary>
    public class EmployeesManagedPackagesViewModel : EmployeesViewModelBase
    {
        private readonly IEmployeeService employeesService;
        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="employeesService"></param>
        public EmployeesManagedPackagesViewModel(IEmployeeService employeesService)
        {
            this.employeesService = employeesService;
            Employees = employeesService.GetAll();
        }

        protected override IQueryable<Employee> GetEmployees()
        {
            return employeesService.GetAll();
        }
    }
}