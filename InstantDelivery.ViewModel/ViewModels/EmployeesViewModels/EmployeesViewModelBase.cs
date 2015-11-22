using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
            CurrentPage = 1;
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
            var pageDto = employeesService.GetPage(CurrentPage, PageSize, GetFilter(),
                SortProperty, SortDirection);
            PageCount = pageDto.PageCount;
            Employees = pageDto.PageCollection;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateData();
        }

        private Expression<Func<Employee, bool>> GetFilter()
        {
            return e => (FirstNameFilter == "" || e.FirstName.StartsWith(FirstNameFilter)) &&
                        (LastNameFilter == "" || e.LastName.StartsWith(LastNameFilter)) &&
                        (EmailFilter == "" || e.Email.StartsWith(EmailFilter));
        }
    }
}
