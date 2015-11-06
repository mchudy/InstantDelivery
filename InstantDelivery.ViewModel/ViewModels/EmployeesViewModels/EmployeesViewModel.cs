using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System.Linq;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : Screen
    {
        private readonly EmployeeService repository;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;
        private IQueryable<Employee> employees;
        private string emailFilter = string.Empty;
        private string firstNameFilter = string.Empty;
        private string lastNameFilter = string.Empty;
        private EmployeeSortingProperty? sortingProperty;
        private int currentPage = 1;

        public EmployeesViewModel(EmployeeService repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Employees = repository.GetAll();
        }

        public IQueryable<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                NotifyOfPropertyChange();
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => IsSelectedAnyRow);
            }
        }

        public bool IsSelectedAnyRow => SelectedEmployee != null;

        public void EditEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new EmployeeEditViewModel
            {
                Employee = SelectedEmployee
            });
            if (result != true)
            {
                repository.Reload(SelectedEmployee);
            }
            else
            {
                repository.Save();
            }
        }

        public void RemoveEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new ConfirmDeleteViewModel());
            if (result == true)
            {
                repository.RemoveEmployee(SelectedEmployee);
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

        private void UpdateEmployees()
        {
            var newEmployees = repository.GetAll();
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
}