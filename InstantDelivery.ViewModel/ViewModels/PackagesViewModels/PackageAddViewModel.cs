using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;
using PropertyChanged;
using System;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku dodawania paczki.
    /// </summary>
    [ImplementPropertyChanged]
    public class PackageAddViewModel : Screen
    {
        private readonly PackagesServiceProxy service;

        public PackageAddViewModel(PackagesServiceProxy service)
        {
            NewPackage = new PackageDto();
            this.service = service;
            this.service = service;
        }

        /// <summary>
        /// Odświeża koszt dostarczenia paczki
        /// </summary>
        public async void RefreshCost()
        {
            Cost = await service.CalculatePackageCost(NewPackage);
        }

        public decimal Cost { get; set; }

        /// <summary>
        /// Aktualnie tworzona paczka.
        /// </summary>
        public PackageDto NewPackage { get; set; }

        /// <summary>
        /// Zapisuje zmiany dokonane w widoku.
        /// </summary>
        public async void Save()
        {
            var packageToSave = NewPackage;
            TryClose(true);
            await service.RegisterPackage(packageToSave);
        }

        /// <summary>
        /// Anuluje zmiany dokonane w widoku.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(true);
        }
    }
}
