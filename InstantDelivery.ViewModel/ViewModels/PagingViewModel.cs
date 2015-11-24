using Caliburn.Micro;
using System.ComponentModel;
using System.Windows.Controls;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Bazowy model widoku dla ekranów wspierających paginację
    /// </summary>
    public class PagingViewModel : Screen
    {
        private int pageSize = 30;
        private int pageCount = 1;
        private int currentPage = 1;

        /// <summary>
        /// Aktualna strona.
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                NotifyOfPropertyChange();
                UpdateData();
            }
        }

        /// <summary>
        /// Aktualny rozmiar strony
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                NotifyOfPropertyChange();
                UpdateData();
            }
        }

        /// <summary>
        /// Aktualna liczba stron (po zastosowaniu filtrów)
        /// </summary>
        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Nazwa właściwości, po której przeprowadzane jest sortowanie
        /// </summary>
        public string SortProperty { get; private set; }

        /// <summary>
        /// Kierunek sortowania
        /// </summary>
        public ListSortDirection? SortDirection { get; private set; }

        /// <summary>
        /// Sortuje dane i przechodzi do pierwszej strony
        /// </summary>
        /// <param name="e"></param>
        public void Sort(DataGridSortingEventArgs e)
        {
            // has to be done manually, SortDirection is set to null every time ItemsSource changes
            if (SortDirection == ListSortDirection.Descending || e.Column.SortMemberPath != SortProperty)
            {
                SortDirection = ListSortDirection.Ascending;
            }
            else
            {
                SortDirection = ListSortDirection.Descending;
            }
            SortProperty = e.Column.SortMemberPath;
            CurrentPage = 1;
            e.Handled = true;
            UpdateData();
        }

        /// <summary>
        /// Uaktualnia dane w tabeli
        /// </summary>
        protected virtual void UpdateData() { }

        protected override void OnActivate()
        {
            base.OnActivate();
            UpdateData();
        }
    }
}