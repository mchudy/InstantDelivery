using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;

namespace InstantDelivery.ViewModel
{
    public class GeneralPackagesViewModel : PackagesViewModelBase
    {
        private readonly IPackageService repository;
        private readonly IWindowManager windowManager;

        public GeneralPackagesViewModel(IPackageService repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Packages = repository.GetAll();

        }

        public Package SelectedPackage { get; set; }

        protected override IQueryable<Package> GetPackages()
        {
            return repository.GetAll();
        }


        public bool IsSelectedAnyRow => SelectedPackage != null;

        public void EditPackage()
        {
            if (SelectedPackage == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new PackageEditViewModel
            {
                service = repository,
                Package = SelectedPackage
            });
            if (result != true)
            {
                repository.Reload(SelectedPackage);
            }
            else
            {
                repository.Save();
            }
        }

        public void RemovePackage()
        {
            if (SelectedPackage == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new ConfirmDeleteViewModel());
            if (result == true)
            {
                repository.RemovePackage(SelectedPackage);
                UpdatePackages();

            }
        }
    }
}