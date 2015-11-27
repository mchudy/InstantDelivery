using InstantDelivery.Model;
using InstantDelivery.Services;
using InstantDelivery.Services.Paging;
using InstantDelivery.ViewModel.Proxies;
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
        private IList<EmployeeDto> employees;
        private readonly IEmployeeService employeesService;

        private string emailFilter = string.Empty;
        private string firstNameFilter = string.Empty;
        private string lastNameFilter = string.Empty;

        private Expression<Func<EmployeeDto, bool>> filter =>
            e => (string.IsNullOrEmpty(FirstNameFilter) || e.FirstName.StartsWith(firstNameFilter)) &&
                 (string.IsNullOrEmpty(LastNameFilter) || e.LastName.StartsWith(LastNameFilter)) &&
                 (string.IsNullOrEmpty(EmailFilter) || e.Email.StartsWith(EmailFilter));

        protected EmployeesViewModelBase(IEmployeeService employeesService)
        {
            this.employeesService = employeesService;
        }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych.
        /// </summary>
        public IList<EmployeeDto> Employees
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

        protected override async void UpdateData()
        {
            var query = new PageQuery<EmployeeDto>
            {
                PageSize = PageSize,
                PageIndex = CurrentPage,
                SortProperty = SortProperty,
                SortDirection = SortDirection,
            };
            query.Filters.Add(filter);
            var pageDto = await new EmployeesServiceProxy().Page(query);
            PageCount = pageDto.PageCount;
            Employees = pageDto.PageCollection;
        }
    }
}
