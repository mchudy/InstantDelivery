using System;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels.EmployeesViewModels;
using System.Collections.Generic;

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
            //Employees = employeesService.GetAll();
        }

        protected override IList<Employee> GetEmployees()
        {
            PageCount = (int)Math.Ceiling((double)(employeesService.Count() / PageSize));
            return employeesService.GetPage(CurrentPage, PageSize);
        }
    }
}