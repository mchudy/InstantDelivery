using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;

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

        protected override void OnDeactivate(bool close)
        {
            if (close)
                NewPackage = null;
        }

        public override void CanClose(Action<bool> callback)
        {
            callback(true);
        }

        public void RefreshCost()
        {
            service.CalculatePackageCost(NewPackage);
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
