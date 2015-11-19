using Caliburn.Micro;
using InstantDelivery.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
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
                UpdateEmployees();
            }
        }

        public async void PageChanged()
        {
            Employees = GetEmployees();
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
            SortProperty = e.Column.SortMemberPath;
            UpdateEmployees();
        }

        protected abstract IList<Employee> GetEmployees();

        protected void UpdateEmployees()
        {
            Employees = GetEmployees();
            //await Task.Run(() =>
            //{
            //    var newEmployees = GetEmployees();
            //    if (SortingProperty != null)
            //    {
            //        newEmployees = SortEmployees(newEmployees);
            //    }
            //    newEmployees = FilterEmployees(newEmployees);
            //    Employees = newEmployees;
            //});
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateEmployees();
        }

        private IQueryable<Employee> SortEmployees(IQueryable<Employee> newEmployees)
        {
            return null;
            //if (SortingProperty == EmployeeSortingProperty.ByFirstName)
            //{
            //    newEmployees = newEmployees.OrderBy(e => e.FirstName);
            //    CurrentPage = 1;
            //}
            //else if (SortingProperty == EmployeeSortingProperty.ByLastName)
            //{
            //    newEmployees = newEmployees.OrderBy(e => e.LastName);
            //    CurrentPage = 1;
            //}
            //return newEmployees;
        }

        private IQueryable<Employee> FilterEmployees(IQueryable<Employee> newEmployees)
        {
            return null;
            //return newEmployees
            //   .Where(e => FirstNameFilter == "" || e.FirstName.StartsWith(FirstNameFilter))
            //   .Where(e => LastNameFilter == "" || e.LastName.StartsWith(LastNameFilter))
            //   .Where(e => EmailFilter == "" || e.Email.StartsWith(EmailFilter));
        }
    }
}
