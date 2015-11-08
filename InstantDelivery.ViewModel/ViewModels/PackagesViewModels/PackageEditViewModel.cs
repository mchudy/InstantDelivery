using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System.Linq;
using System.Threading.Tasks;
using Screen = Caliburn.Micro.Screen;

namespace InstantDelivery.ViewModel
{
    public class PackageEditViewModel : Screen
    {
        private readonly IPackageService service;

        public PackageEditViewModel(IPackageService service)
        {
            this.service = service;
        }

        private IQueryable<Employee> employees;
        private Employee selectedEmployee;
        public Package Package { get; set; }

        public IQueryable<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                NotifyOfPropertyChange();
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                NotifyOfPropertyChange();
            }
        }

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