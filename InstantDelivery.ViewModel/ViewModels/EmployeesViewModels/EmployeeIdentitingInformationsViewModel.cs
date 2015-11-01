using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;
using InstantDelivery.ViewModel.ViewModels;

namespace InstantDelivery.ViewModel
{
    public class EmployeeIdentitingInformationsViewModel : PagingViewModel
    {
        private readonly EmployeesRepository repository;
        private BindableCollection<Employee> rows;

        public EmployeeIdentitingInformationsViewModel(EmployeesRepository repository)
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