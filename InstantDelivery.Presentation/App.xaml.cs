using InstantDelivery.Core;
using System.Data.Entity;
using System.Windows;

namespace InstantDelivery
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Database.SetInitializer(new InstantDeliveryInitializer());
        }
    }
}
