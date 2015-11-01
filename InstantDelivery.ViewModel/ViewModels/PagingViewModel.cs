using Caliburn.Micro;

namespace InstantDelivery.ViewModel.ViewModels
{
    // TODO to nadal jest źle zrobione i trzeba to poprawić, ale chociaż minimalnie zmiejsza ten syf w viewmodelach
    public abstract class PagingViewModel : Screen
    {
        private int currentPage = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
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

        public void NextPage()
        {
            CurrentPage++;
            LoadPage();
        }

        public abstract bool IsEnabledNextPage { get; }

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

        protected abstract void LoadPage();
    }
}
