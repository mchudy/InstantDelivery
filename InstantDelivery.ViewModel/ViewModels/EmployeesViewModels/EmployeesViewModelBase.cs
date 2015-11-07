using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using System.ComponentModel;
using System.Linq;

namespace InstantDelivery.ViewModel.ViewModels.EmployeesViewModels
{
    public abstract class EmployeesViewModelBase : Screen
    {
        private IQueryable<Employee> employees;

        private int currentPage = 1;
        private string emailFilter = string.Empty;
        private string firstNameFilter = string.Empty;
        private string lastNameFilter = string.Empty;
        private EmployeeSortingProperty? sortingProperty;

        public IQueryable<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                NotifyOfPropertyChange();
            }
        }

        public string LastNameFilter
        {
            get { return lastNameFilter; }
            set
            {
                lastNameFilter = value;
                UpdateEmployees();
            }
        }

        public string FirstNameFilter
        {
            get { return firstNameFilter; }
            set
            {
                firstNameFilter = value;
                UpdateEmployees();
            }
        }

        protected abstract IQueryable<Employee> GetEmployees();

        protected void UpdateEmployees()
        {
            var newEmployees = GetEmployees();
            if (SortingProperty != null)
            {
                newEmployees = SortEmployees(newEmployees);
            }
            newEmployees = FilterEmployees(newEmployees);
            Employees = newEmployees;
        }

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyOfPropertyChange();
            }
        }

        public string EmailFilter
        {
            get { return emailFilter; }
            set
            {
                emailFilter = value;
                UpdateEmployees();
            }
        }

        public EmployeeSortingProperty? SortingProperty
        {
            get { return sortingProperty; }
            set
            {
                sortingProperty = value;
                UpdateEmployees();
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateEmployees();
        }

        private IQueryable<Employee> SortEmployees(IQueryable<Employee> newEmployees)
        {
            if (SortingProperty == EmployeeSortingProperty.ByFirstName)
            {
                newEmployees = newEmployees.OrderBy(e => e.FirstName);
                CurrentPage = 1;
            }
            else if (SortingProperty == EmployeeSortingProperty.ByLastName)
            {
                newEmployees = newEmployees.OrderBy(e => e.LastName);
                CurrentPage = 1;
            }
            return newEmployees;
        }

        private IQueryable<Employee> FilterEmployees(IQueryable<Employee> newEmployees)
        {
            return newEmployees
                .Where(e => FirstNameFilter == "" || e.FirstName.StartsWith(FirstNameFilter))
                .Where(e => LastNameFilter == "" || e.LastName.StartsWith(LastNameFilter))
                .Where(e => EmailFilter == "" || e.Email.StartsWith(EmailFilter));
        }
    }

    public enum EmployeeSortingProperty
    {
        [Description("Po nazwisku")]
        ByLastName,
        [Description("Po imieniu")]
        ByFirstName,
    }
}
