using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;
using InstantDelivery.ViewModel.ViewModels;
using System.ComponentModel;

namespace InstantDelivery.ViewModel
{
    public class EmployeesUsedVehiclesViewModel : PagingViewModel
    {
        private readonly EmployeeService repository;
        private readonly IWindowManager windowManager;
        private Employee selectedRow;
        private BindableCollection<Employee> rows;

        public EmployeesUsedVehiclesViewModel(EmployeeService repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Rows = new BindableCollection<Employee>(repository.Page(CurrentPage, PageSize));
        }

        public Employee SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
                NotifyOfPropertyChange();
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsEnabledViewDetails)));
            }
        }

        public bool IsEnabledViewDetails => SelectedRow != null;

        public void ShowVehicleDetails()
        {
            if (SelectedRow == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new EmployeeUsedVehiclesDetailsViewModel
            {
                SelectedRow = SelectedRow
            });
            if (result != true)
            {
                repository.Reload(SelectedRow);
            }
            else
            {
                repository.Save();
            }
        }

        public BindableCollection<Employee> Rows
        {
            get { return rows; }
            set
            {
                rows = value;
                NotifyOfPropertyChange();
            }
        }

        public override bool IsEnabledNextPage => CurrentPage * PageSize < repository.Total;

        protected override void LoadPage()
        {
            Rows = new BindableCollection<Employee>(repository.Page(CurrentPage, PageSize));
        }
    }
}