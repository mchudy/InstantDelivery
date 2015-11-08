using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using PropertyChanged;
using System.Linq;
using System.Threading.Tasks;
using Screen = Caliburn.Micro.Screen;

namespace InstantDelivery.ViewModel
{
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

        public async void Save()
        {
            TryClose(true);
            if (Package.Status != PackageStatus.Delivered && SelectedEmployee != null)
            {
                service.AssignPackage(Package, SelectedEmployee);
            }
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