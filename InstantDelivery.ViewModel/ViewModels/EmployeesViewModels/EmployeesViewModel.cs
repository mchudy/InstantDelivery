using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;
using InstantDelivery.ViewModel.ViewModels;

namespace InstantDelivery.ViewModel
{
    public class EmployeesViewModel : PagingViewModel
    {
        private readonly EmployeesRepository repository;
        private readonly IWindowManager windowManager;
        private Employee selectedEmployee;
        private BindableCollection<Employee> employees;

        public EmployeesViewModel(EmployeesRepository repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Employees = new BindableCollection<Employee>(repository.Page(CurrentPage, PageSize));
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

        public BindableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                NotifyOfPropertyChange();
            }
        }

        public override bool IsEnabledNextPage => CurrentPage * PageSize < repository.Total;

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

        public void DeleteEmployee()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new EmployeeDeleteViewModel());
            if (result == true)
            {
                repository.Remove(SelectedEmployee);
                LoadPage();
            }
        }

        protected override void LoadPage()
        {
            Employees = new BindableCollection<Employee>(repository.Page(CurrentPage, PageSize));
        }
    }
}