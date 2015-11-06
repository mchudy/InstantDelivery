using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using InstantDelivery.ViewModel.ViewModels;

namespace InstantDelivery.ViewModel
{
    public class EmployeesManagedPackagesViewModel : PagingViewModel
    {
        private readonly EmployeeService repository;
        private BindableCollection<Employee> rows;

        public EmployeesManagedPackagesViewModel(EmployeeService repository)
        {
            this.repository = repository;
            Rows = new BindableCollection<Employee>(repository.Page(CurrentPage, PageSize));
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