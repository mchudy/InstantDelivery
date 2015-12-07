using InstantDelivery.Common.Enums;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using PropertyChanged;
using System.Collections.Generic;
using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Packages;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku edycji paczki.
    /// </summary>
    [ImplementPropertyChanged]
    public class PackageEditViewModel : PagingViewModel
    {
        private readonly PackagesServiceProxy packagesService;

        /// <summary>
        /// Tworzy nowy model widoku edycji paczki
        /// </summary>
        /// <param name="packagesService"></param>
        public PackageEditViewModel(PackagesServiceProxy packagesService)
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
        public PackageDto Package { get; set; }

        /// <summary>
        /// Zbiór pracowników, którym można przypisać aktualną przesyłkę
        /// </summary>
        public IList<EmployeeDto> Employees { get; set; }

        /// <summary>
        /// Pracownik przypisany do przesyłki
        /// </summary>
        public EmployeeDto SelectedEmployee { get; set; }

        /// <summary>
        /// Zwraca wartość informującą, czy przesyłka jest dostarczone
        /// </summary>
        public bool IsDelivered { get; set; }

        /// <summary>
        /// Zapisuje zmiany dokonane w widoku.
        /// </summary>
        public async void Save()
        {
            if (Package.Status == PackageStatus.New && SelectedEmployee != null)
            {
                await packagesService.AssignPackage(Package.Id, SelectedEmployee.Id);
            }
            else if (Package.Status == PackageStatus.InDelivery && IsDelivered)
            {
                await packagesService.MarkAsDelivered(Package.Id);
            }
            TryClose(true);
        }

        /// <summary>
        /// Anuluje zmiany dokonane w widoku.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }

        /// <summary>
        /// Wylicza na nowo koszt paczki na podstawie jej danych
        /// </summary>
        public async void RefreshCost()
        {
            await packagesService.CalculatePackageCost(Package);
        }

        protected override async void UpdateData()
        {
            var query = GetPageQuery();
            var pageDto = await packagesService.GetAvailableEmployeesPage(Package.Id, query);
            PageCount = pageDto.PageCount;
            Employees = pageDto.PageCollection;
        }
    }
}