using InstantDelivery.ViewModel;
using System.Windows.Controls;

namespace InstantDelivery.Views
{
    /// <summary>
    /// Interaction logic for DisplayMapOfCourierPackagesView.xaml
    /// </summary>
    public partial class CourierPackagesMapView : UserControl
    {
        public CourierPackagesMapView()
        {
            InitializeComponent();
            Browser.Navigate("pack://siteoforigin:,,,/Maps/map.html");
            Loaded += CourierPackagesMapView_Loaded;
        }

        private void CourierPackagesMapView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var dataContext = (CourierPackagesMapViewModel)DataContext;
            dataContext.InvokeScript += DataContextOnInvokeScript;
        }

        private void DataContextOnInvokeScript(object sender, InvokeScriptEventArgs e)
        {
            Browser.InvokeScript(e.FunctionName, e.Parameters);
        }
    }
}
