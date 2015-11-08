using System.Threading.Tasks;
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

        public async void Save()
        {
            var PackageToSave = Package;
            TryClose(true);
            await Task.Run(() =>
            {
                service.CalculatePackageCost(Package);
            });
        }

        public void Cancel()
        {
            TryClose(false);
        }

        public async void RefreshCost()
        {
            await Task.Run(() =>
            {
                service.CalculatePackageCost(Package);
            });
        }
    }
}