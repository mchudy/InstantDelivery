using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku dodawania paczki.
    /// </summary>
    public class PackageAddViewModel : Screen
    {
        private readonly IPackageService service;
        private Package newPackage;

        public PackageAddViewModel(IPackageService service)
        {
            NewPackage = new Package();
            this.service = service;
            this.service = service;
        }

        /// <summary>
        /// Odświeża koszt dostarczenia paczki
        /// </summary>
        public async void RefreshCost()
        {
            NewPackage.Cost = await Task.Run(() => service.CalculatePackageCost(NewPackage));
        }

        /// <summary>
        /// Aktualnie tworzona paczka.
        /// </summary>
        public Package NewPackage
        {
            get { return newPackage; }
            set
            {
                newPackage = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Zapisuje zmiany dokonane w widoku.
        /// </summary>
        public async void Save()
        {
            var packageToSave = NewPackage;
            TryClose(true);
            await Task.Run(() =>
            {
                service.RegisterPackage(packageToSave);
            });
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
