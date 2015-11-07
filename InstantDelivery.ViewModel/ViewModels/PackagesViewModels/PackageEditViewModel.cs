using System.Windows.Forms;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using Screen = Caliburn.Micro.Screen;

namespace InstantDelivery.ViewModel
{
    public class PackageEditViewModel : Screen
    {
        public IPackageService service;
        public Package Package { get; set; }

        public void Save()
        {
            service.CalculatePackageCost(Package);
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }

        public void RefreshCost()
        {
            service.CalculatePackageCost(Package);
        }
    }
}