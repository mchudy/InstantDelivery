using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using System;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    class PackageAddViewModel : Screen
    {
        public IPackageService service;
        public PackageAddViewModel(IPackageService service)
        {
            NewPackage = new Package();
            this.service = service;
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(true);
        }

        public async void RefreshCost()
        {
            NewPackage.Cost = await Task.Run(() => service.CalculatePackageCost(NewPackage));
        }

        private Package newPackage;
        public Package NewPackage
        {
            get { return newPackage; }
            set
            {
                newPackage = value;
                NotifyOfPropertyChange();
            }
        }

        public void Save()
        {
            service.AddPackage(NewPackage);
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
