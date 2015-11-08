using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model woidoku ogólnego paczek.
    /// </summary>
    public class GeneralPackagesViewModel : PackagesViewModelBase
    {
        private readonly IPackageService repository;
        private readonly IWindowManager windowManager;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="windowManager"></param>
        public GeneralPackagesViewModel(IPackageService repository, IWindowManager windowManager)
        {
            this.repository = repository;
            this.windowManager = windowManager;
            Packages = repository.GetAll();

        }
        /// <summary>
        /// Aktualnie zaznaczony wiersz tabeli danych.
        /// </summary>
        public Package SelectedPackage { get; set; }

        protected override IQueryable<Package> GetPackages()
        {
            return repository.GetAll();
        }

        /// <summary>
        /// Flaga informująca o tym czy aktualnie zaznaczony jest jakiś wiersz.
        /// </summary>
        public bool IsSelectedAnyRow => SelectedPackage != null;
        /// <summary>
        /// Delegat zdarzenia przejścia do widoku edycji paczki.
        /// </summary>
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
        /// <summary>
        /// Delegat zdarzenia usuwania paczki przez użytkownika.
        /// </summary>
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