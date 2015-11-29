using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model woidoku ogólnego paczek.
    /// </summary>
    public class GeneralPackagesViewModel : PackagesViewModelBase
    {
        private readonly PackagesServiceProxy service;
        private readonly IWindowManager windowManager;
        private readonly PackageEditViewModel packageEditViewModel;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="service"></param>
        /// <param name="windowManager"></param>
        /// <param name="packageEditViewModel"></param>
        public GeneralPackagesViewModel(PackagesServiceProxy service, IWindowManager windowManager,
            PackageEditViewModel packageEditViewModel)
            : base(service)
        {
            this.service = service;
            this.windowManager = windowManager;
            this.packageEditViewModel = packageEditViewModel;
        }

        /// <summary>
        /// Aktualnie zaznaczony wiersz tabeli danych.
        /// </summary>
        public PackageDto SelectedPackage { get; set; }

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
            packageEditViewModel.Package = SelectedPackage;
            packageEditViewModel.SelectedEmployee = await service.GetAssignedEmployee(SelectedPackage.Id);
            bool? result = windowManager.ShowDialog(packageEditViewModel);
            if (result == true)
            {
                UpdateData();
            }
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

            if (result == true)
            {
                await service.DeletePackage(SelectedPackage.Id);
                UpdateData();
            }
        }
    }
}