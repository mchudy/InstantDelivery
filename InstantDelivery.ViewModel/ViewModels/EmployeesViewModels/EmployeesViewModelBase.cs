using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Paging;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Bazowy model widoku dla innych widoków pracowników.
    /// </summary>
    public abstract class EmployeesViewModelBase : PagingViewModel
    {
        protected BindableCollection<EmployeeDto> employees;

        private EmployeesServiceProxy service;
        private string emailFilter = string.Empty;
        private string firstNameFilter = string.Empty;
        private string lastNameFilter = string.Empty;

        protected EmployeesViewModelBase(EmployeesServiceProxy service)
        {
            this.service = service;
        }

        /// <summary>
        /// Kolekcja skojarzona z tabelą danych.
        /// </summary>
        public virtual BindableCollection<EmployeeDto> Employees
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
            var query = GetPageQuery();
            AddFilters(query);
            var pageDto = await service.Page(query);
            PageCount = pageDto.PageCount;
            Employees = new BindableCollection<EmployeeDto>(pageDto.PageCollection);
        }

        protected void AddFilters(PageQuery query)
        {
            if (!string.IsNullOrEmpty(FirstNameFilter))
            {
                query.Filters[nameof(EmployeeDto.FirstName)] = FirstNameFilter;
            }
            if (!string.IsNullOrEmpty(LastNameFilter))
            {
                query.Filters[nameof(EmployeeDto.LastName)] = LastNameFilter;
            }
            if (!string.IsNullOrEmpty(EmailFilter))
            {
                query.Filters[nameof(EmployeeDto.Email)] = EmailFilter;
            }
        }
    }
}
