using System.Threading.Tasks;
using System.Windows.Forms;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using Screen = Caliburn.Micro.Screen;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku edycji paczki.
    /// </summary>
    public class PackageEditViewModel : Screen
    {
        /// <summary>
        /// Serwis paczek
        /// </summary>
        public IPackageService service;
        /// <summary>
        /// Edytowana paczka
        /// </summary>
        public Package Package { get; set; }
        /// <summary>
        /// Zapisuje zmiany dokonane w widoku.
        /// </summary>
        public async void Save()
        {
            var PackageToSave = Package;
            TryClose(true);
            await Task.Run(() =>
            {
                service.CalculatePackageCost(Package);
            });
        }
        /// <summary>
        /// Anuluje zmiany dokonane w widoku.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }
        /// <summary>
        /// Wylicza na nowo koszt paczki na podstawie jej szczegółów.
        /// </summary>
        public async void RefreshCost()
        {
            await Task.Run(() =>
            {
                service.CalculatePackageCost(Package);
            });
        }
    }
}