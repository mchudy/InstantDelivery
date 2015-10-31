using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : Screen
    {
        private readonly EmployeesRepository repository;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;
        private int currentPage = 1;
        private int pageSize = 10;
        private BindableCollection<Employee> employees;

        public EmployeesViewModel(EmployeesRepository repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Employees = new BindableCollection<Employee>(repository.Page(CurrentPage, pageSize));
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                NotifyOfPropertyChange();
            }
        }

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => IsEnabledPreviousPage);
                NotifyOfPropertyChange(() => IsEnabledNextPage);
            }
        }

        public BindableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                NotifyOfPropertyChange();
            }
        }

        public void NextPage()
        {
            CurrentPage++;
            LoadPage();
        }

        public bool IsEnabledNextPage => currentPage * pageSize < repository.Total;

        public bool IsEnabledPreviousPage => currentPage != 1;

        public void PreviousPage()
        {
            if (CurrentPage == 1) return;
            CurrentPage--;
            LoadPage();
        }

        public void Sort()
        {
            CurrentPage = 1;
            LoadPage();
        }

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

        private void LoadPage()
        {
            Employees = new BindableCollection<Employee>(repository.Page(CurrentPage, pageSize));
        }
    }
}