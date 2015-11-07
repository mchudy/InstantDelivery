using InstantDelivery.Annotations;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InstantDelivery.Controls
{
    /// <summary>
    /// Kontrolka obsługująca paginację
    /// </summary>
    public partial class DataPager : UserControl, INotifyPropertyChanged
    {
        private const int initiallPageSize = 30;
        private int pagesCount;

        /// <summary>
        /// Tworzy nową kontrolkę
        /// </summary>
        public DataPager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Źródło danych wykorzystywane do paginacji
        /// </summary>
        public IQueryable<Entity> ItemsSource
        {
            get { return (IQueryable<Entity>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IQueryable<Entity>),
              typeof(DataPager), new UIPropertyMetadata(null, OnPropertyChanged));

        /// <summary>
        /// Kolekcja zawierająca aktualną stronę danych ze źródłówej kolekcji
        /// </summary>
        public ObservableCollection<object> PagedSource
        {
            get { return (ObservableCollection<object>)GetValue(PagedSourceProperty); }
            set { SetValue(PagedSourceProperty, value); }
        }

        public static readonly DependencyProperty PagedSourceProperty =
            DependencyProperty.Register("PagedSource", typeof(ObservableCollection<object>),
              typeof(DataPager));

        /// <summary>
        /// Aktualny numer strony
        /// </summary>
        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int),
              typeof(DataPager), new UIPropertyMetadata(1, OnPropertyChanged));

        /// <summary>
        /// Aktualny rozmiar strony
        /// </summary>
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int),
              typeof(DataPager), new UIPropertyMetadata(initiallPageSize, OnPropertyChanged));

        public int PagesCount
        {
            get { return pagesCount; }
            set
            {
                pagesCount = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabledNextPage => CurrentPage * PageSize < ItemsSource?.Count();

        public bool IsEnabledPreviousPage => CurrentPage > 1;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MoveToNextPage(object sender, RoutedEventArgs routedEventArgs)
        {
            CurrentPage++;
        }

        private void MoveToPreviousPage(object sender, RoutedEventArgs routedEventArgs)
        {
            CurrentPage--;
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataPager control = (DataPager)d;
            control.LoadPage();
        }

        private async void LoadPage()
        {
            if (ItemsSource == null) return;
            PagesCount = (int)Math.Ceiling((double)ItemsSource.Count() / PageSize);
            if (CurrentPage > PagesCount)
            {
                CurrentPage = PagesCount == 0 ? 1 : PagesCount;
            }
            ObservableCollection<object> collection = null;
            var itemsSource = ItemsSource;
            var currentPage = CurrentPage;
            var pageSize = PageSize;
            await Task.Run(() =>
            {
                collection = new ObservableCollection<object>(itemsSource.Page(currentPage, pageSize));
            });
            PagedSource = collection;
            OnPropertyChanged(nameof(IsEnabledNextPage));
            OnPropertyChanged(nameof(IsEnabledPreviousPage));
        }
    }
}
