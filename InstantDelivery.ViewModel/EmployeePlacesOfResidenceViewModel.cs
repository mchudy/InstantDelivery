using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Repositories;

namespace InstantDelivery.ViewModel
{
    public class EmployeePlacesOfResidenceViewModel : Screen
    {
        private readonly EmployeesRepository repository;
        private readonly IWindowManager windowManager;
        private int currentPage = 1;
        private int pageSize = 20;
        private BindableCollection<Employee> rows;

        public EmployeePlacesOfResidenceViewModel(EmployeesRepository repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Rows = new BindableCollection<Employee>(repository.Page(CurrentPage, pageSize));
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

        public BindableCollection<Employee> Rows
        {
            get { return rows; }
            set
            {
                rows = value;
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

        private void LoadPage()
        {
            Rows = new BindableCollection<Employee>(repository.Page(CurrentPage, pageSize));
        }
    }
}