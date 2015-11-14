using InstantDelivery.Services;
using PropertyChanged;
using System.Linq;
using System.Threading.Tasks;
using InstantDelivery.Domain.Entities;
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

        /// <summary>
        /// Tworzy nowy model widoku edycji paczki
        /// </summary>
        /// <param name="service"></param>
        public PackageEditViewModel(IPackageService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Flaga informująca, czy jest możliwa edycja danych przesyłki
        /// </summary>
        public bool IsPackageDataReadOnly => Package.Status != PackageStatus.New;

        /// <summary>
        /// Aktualnie edytowana przesyłka
        /// </summary>
        public Package Package { get; set; }

        /// <summary>
        /// Zbiór pracowników, którym można przypisać aktualną przesyłkę
        /// </summary>
        public IQueryable<Employee> Employees { get; set; }

        /// <summary>
        /// Pracownik przypisany do przesyłki
        /// </summary>
        public Employee SelectedEmployee { get; set; }

        /// <summary>
        /// Zwraca wartość informującą, czy przesyłka jest dostarczone
        /// </summary>
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