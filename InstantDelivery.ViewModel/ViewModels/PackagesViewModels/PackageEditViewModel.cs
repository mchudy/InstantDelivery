using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using InstantDelivery.Services.Paging;
using PropertyChanged;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku edycji paczki.
    /// </summary>
    [ImplementPropertyChanged]
    public class PackageEditViewModel : PagingViewModel
    {
        private readonly IPackageService packagesService;

        /// <summary>
        /// Tworzy nowy model widoku edycji paczki
        /// </summary>
        /// <param name="packagesService"></param>
        public PackageEditViewModel(IPackageService packagesService)
        {
            this.packagesService = packagesService;
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
        public IList<Employee> Employees { get; set; }

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
                packagesService.AssignPackage(Package, SelectedEmployee);
            }
            else if (Package.Status == PackageStatus.InDelivery && IsDelivered)
            {
                packagesService.MarkAsDelivered(Package);
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
                packagesService.CalculatePackageCost(Package);
            });
        }

        protected override void UpdateData()
        {
            var query = new PageQuery<Employee>
            {
                PageSize = PageSize,
                PageIndex = CurrentPage,
                SortProperty = SortProperty,
                SortDirection = SortDirection,
            };
            var pageDto = packagesService.GetAvailableEmployees(Package, query);
            PageCount = pageDto.PageCount;
            Employees = pageDto.PageCollection;
        }
    }
}