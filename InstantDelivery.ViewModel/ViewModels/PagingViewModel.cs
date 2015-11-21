using Caliburn.Micro;
using System.ComponentModel;
using System.Windows.Controls;

namespace InstantDelivery.ViewModel
{
    public class PagingViewModel : Screen
    {
        private int pageSize;
        private int pageCount;
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
            }
        }

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                NotifyOfPropertyChange();
            }
        }

        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                NotifyOfPropertyChange();
            }
        }

        public string SortProperty { get; private set; }

        public ListSortDirection? SortDirection { get; private set; }

        public void Sort(DataGridSortingEventArgs e)
        {
            //TODO: fix arrows
            // has to be done manually, property of the column does not get updated
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

        public virtual void UpdateData()
        {
        }

    }
}