using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using System;
using System.Collections.Generic;

namespace InstantDelivery.ViewModel.ViewModels.EmployeesViewModels
{
    /// <summary>
    /// Bazowy model widoku dla innych widoków pracowników.
    /// </summary>
    public abstract class EmployeesViewModelBase : PagingViewModel
    {
        private IList<Employee> employees;
        private readonly IEmployeeService employeesService;

        private string emailFilter = string.Empty;
        private string firstNameFilter = string.Empty;
        private string lastNameFilter = string.Empty;

        protected EmployeesViewModelBase(IEmployeeService employeesService)
        {
            this.employeesService = employeesService;
            PageSize = 30;
            PageCount = (int)Math.Ceiling(employeesService.Count() / (double)PageSize);
            Employees = employeesService.GetPage(1, PageSize);
        }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych.
        /// </summary>
        public IList<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Filtr nazwiska wybrany przez użytkownika.
        /// </summary>
        public string LastNameFilter
        {
            get { return lastNameFilter; }
            set
            {
                lastNameFilter = value;
                CurrentPage = 1;
                UpdateData();
            }
        }

        /// <summary>
        /// Filtr imienia wybrany przez użytkownika.
        /// </summary>
        public string FirstNameFilter
        {
            get { return firstNameFilter; }
            set
            {
                firstNameFilter = value;
                CurrentPage = 1;
                UpdateData();
            }
        }

        /// <summary>
        /// Filtr adresu email wybrany przez użytkownika.
        /// </summary>
        public string EmailFilter
        {
            get { return emailFilter; }
            set
            {
                emailFilter = value;
                CurrentPage = 1;
                UpdateData();
            }
        }

        public void PageChanged()
        {
            UpdateData();
        }

        public override void UpdateData()
        {
            int pageCount;
            var page = employeesService.GetPage(CurrentPage, PageSize, FirstNameFilter, LastNameFilter, EmailFilter,
                SortProperty, SortDirection, out pageCount);
            PageCount = pageCount;
            Employees = page;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateData();
        }
    }
}
