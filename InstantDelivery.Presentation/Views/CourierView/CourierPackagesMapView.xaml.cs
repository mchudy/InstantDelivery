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
        }
    }
}
