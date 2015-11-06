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
        private BindableCollection<Employee> employees;
        private IQueryable<Employee> items;

        public EmployeesViewModel(EmployeeService repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Items = repository.GetAll().AsQueryable();
            //Employees = new BindableCollection<Employee>(repository.Page(CurrentPage, PageSize));
        }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public IQueryable<Employee> Items
        {
            get { return items; }
            set
            {
                items = value;
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

        //public bool IsEnabledNextPage => CurrentPage * PageSize < repository.Total;

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
                LoadPage();
            }
        }

        protected void LoadPage()
        {
            //OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsEnabledNextPage)));
            //OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsEnabledNextPage)));
        }

        public void SetLastNameFilter(string Text)
        {
            repository.LastNameFilter = Text;
            LoadPage();
        }

        public void SetFirstNameFilter(string Text)
        {
            repository.FirstNameFilter = Text;
            LoadPage();
        }

        public void SetEmailFilter(string Text)
        {
            repository.EmailFilter = Text;
            LoadPage();
        }

        public void SetSortingFilter(EmployeeSortingFilter filter)
        {
            repository.SortingFilter = filter;
            LoadPage();
        }

        public void ChangePage()
        {
            //Employees = new BindableCollection<Employee>(repository.Page(CurrentPage, PageSize));
        }
    }
}