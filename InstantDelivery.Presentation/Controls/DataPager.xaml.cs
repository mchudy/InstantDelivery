using InstantDelivery.Annotations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InstantDelivery.Controls
{
    /// <summary>
    /// Kontrolka obsługująca paginację
    /// </summary>
    public partial class DataPager : UserControl, INotifyPropertyChanged
    {
        private const int initialPageSize = 30;

        /// <summary>
        /// Tworzy nową kontrolkę
        /// </summary>
        public DataPager()
        {
            InitializeComponent();
        }

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
                typeof(DataPager), new UIPropertyMetadata(1, OnPageChanged));

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
                typeof(DataPager), new UIPropertyMetadata(initialPageSize, OnPageChanged));

        /// <summary>
        /// Liczba stron
        /// </summary>
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int),
                typeof(DataPager), new UIPropertyMetadata(1, OnPageCountChanged));


        /// <summary>
        /// Zwraca wartość wskazującą czy możliwe jest przejście na następną stronę
        /// </summary>
        public bool IsEnabledNextPage => CurrentPage < PageCount;

        /// <summary>
        /// Zwraca wartość wskazującą czy możliwe jest przejście na poprzednią stronę
        /// </summary>
        public bool IsEnabledPreviousPage => CurrentPage > 1;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MoveToPreviousPage(object sender, RoutedEventArgs routedEventArgs)
        {
            if (CurrentPage - 1 < 1)
            {
                CurrentPage = 1;
            }
            else
            {
                CurrentPage--;
            }
        }

        private void MoveToNextPage(object sender, RoutedEventArgs routedEventArgs)
        {
            if (CurrentPage + 1 > PageCount)
            {
                CurrentPage = PageCount;
            }
            else
            {
                CurrentPage++;
            }
        }

        private static void OnPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataPager control = (DataPager)d;
            control.OnPropertyChanged(nameof(IsEnabledPreviousPage));
            control.OnPropertyChanged(nameof(IsEnabledNextPage));
            if (control.CurrentPage > control.PageCount)
            {
                control.CurrentPage = control.PageCount;
            }
        }

        private static void OnPageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataPager control = (DataPager)d;
            control.OnPropertyChanged(nameof(IsEnabledNextPage));
        }
    }
}
