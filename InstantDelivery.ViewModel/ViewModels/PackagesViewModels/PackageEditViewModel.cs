using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using PropertyChanged;
using System.Linq;
using System.Threading.Tasks;
using Screen = Caliburn.Micro.Screen;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku edycji paczki.
    /// </summary>
    [ImplementPropertyChanged]
    public class PackageEditViewModel : Screen
    {
        private readonly IPackageService service;

        public PackageEditViewModel(IPackageService service)
        {
            this.service = service;
        }

        public bool IsPackageDataReadOnly => Package.Status != PackageStatus.New;

        public Package Package { get; set; }
        public IQueryable<Employee> Employees { get; set; }
        public Employee SelectedEmployee { get; set; }
        public bool IsDelivered { get; set; }

        /// <summary>
        /// Zapisuje zmiany dokonane w widoku.
        /// </summary>
        public void Save()
        {
            TryClose(true);
            if (Package.Status == PackageStatus.New && SelectedEmployee != null)
            {
                service.AssignPackage(Package, SelectedEmployee);
            }
            else if (Package.Status == PackageStatus.InDelivery && IsDelivered)
            {
                service.MarkAsDelivered(Package);
            }
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