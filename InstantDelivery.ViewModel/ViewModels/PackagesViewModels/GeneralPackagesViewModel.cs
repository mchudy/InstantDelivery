using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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

        public async void EditPackage()
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
            await Task.Run(() =>
            {
                if (result != true)
                {
                    repository.Reload(SelectedPackage);
                }
                else
                {
                    repository.Save();
                }
            });
        }

        public async void RemovePackage()
        {
            if (SelectedPackage == null)
            {
                return;
            }
            var result = windowManager.ShowDialog(new ConfirmDeleteViewModel());

            await Task.Run(() =>
            {
                if (result == true)
                {
                    repository.RemovePackage(SelectedPackage);
                    UpdatePackages();

                }
            });
        }
    }
}