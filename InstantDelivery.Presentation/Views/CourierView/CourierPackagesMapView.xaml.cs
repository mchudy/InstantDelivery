using InstantDelivery.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            Browser.LoadCompleted += Browser_LoadCompleted;
        }

        private void Browser_LoadCompleted(object sender, NavigationEventArgs navigationEventArgs)
        {
            var dataContext = (CourierPackagesMapViewModel)DataContext;
            var json = JsonConvert.SerializeObject(dataContext.Packages,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            Browser.InvokeScript("showPackages", json);
        }
    }
}
