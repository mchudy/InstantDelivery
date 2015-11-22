using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using InstantDelivery.Services.Paging;
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

        private Expression<Func<Employee, bool>> filter =>
            e => (string.IsNullOrEmpty(FirstNameFilter) || e.FirstName.StartsWith(firstNameFilter)) &&
                 (string.IsNullOrEmpty(LastNameFilter) || e.LastName.StartsWith(LastNameFilter)) &&
                 (string.IsNullOrEmpty(EmailFilter) || e.Email.StartsWith(EmailFilter));

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
            var query = new PageQuery<Employee>
            {
                PageSize = PageSize,
                PageIndex = CurrentPage,
                SortProperty = SortProperty,
                SortDirection = SortDirection,
            };
            query.Filters.Add(filter);
            var pageDto = employeesService.GetPage(query);
            PageCount = pageDto.PageCount;
            Employees = pageDto.PageCollection;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateData();
        }
    }
}
