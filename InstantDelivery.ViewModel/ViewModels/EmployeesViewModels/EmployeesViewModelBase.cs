using Caliburn.Micro;
using InstantDelivery.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InstantDelivery.ViewModel.ViewModels.EmployeesViewModels
{
    /// <summary>
    /// Bazowy model widoku dla innych widoków pracowników.
    /// </summary>
    public abstract class EmployeesViewModelBase : Screen
    {
        private IList<Employee> employees;

        private int currentPage = 1;
        private string emailFilter = string.Empty;
        private string firstNameFilter = string.Empty;
        private string lastNameFilter = string.Empty;
        private int pageSize;
        private int pageCount;


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
                UpdateEmployees();
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
                UpdateEmployees();
            }
        }

        /// <summary>
        /// Aktualna strona.
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyOfPropertyChange();
            }
        }

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                NotifyOfPropertyChange();
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
                UpdateEmployees();
            }
        }

        public async void PageChanged()
        {
            Employees = await Task.Run(() => GetEmployees());
        }

        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                NotifyOfPropertyChange();
            }
        }


        public string SortProperty { get; private set; }

        //TODO: unnecessary dependency on PresentationFramework, pass only SortMemberPath
        public void Sort(DataGridSortingEventArgs e)
        {
            //TODO: fix arrows
            // has to be done manually, property of the column does not get updated
            if (SortDirection == ListSortDirection.Descending || e.Column.SortMemberPath != SortProperty)
            {
                SortDirection = ListSortDirection.Ascending;
            }
            else
            {
                SortDirection = ListSortDirection.Descending;
            }
            SortProperty = e.Column.SortMemberPath;
            CurrentPage = 1;
            UpdateEmployees();
            e.Handled = true;
        }

        public ListSortDirection? SortDirection { get; private set; }

        protected abstract IList<Employee> GetEmployees();

        protected void UpdateEmployees()
        {
            Employees = GetEmployees();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateEmployees();
        }
    }
}
