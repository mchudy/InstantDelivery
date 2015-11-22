using Caliburn.Micro;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model woidoku ogólnego paczek.
    /// </summary>
    public class GeneralPackagesViewModel : PackagesViewModelBase
    {
        private readonly IPackageService service;
        private readonly IWindowManager windowManager;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="service"></param>
        /// <param name="windowManager"></param>
        public GeneralPackagesViewModel(IPackageService service, IWindowManager windowManager)
            : base(service)
        {
            this.service = service;
            this.windowManager = windowManager;
        }

        /// <summary>
        /// Aktualnie zaznaczony wiersz tabeli danych.
        /// </summary>
        public Package SelectedPackage { get; set; }

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
            var result = windowManager.ShowDialog(new PackageEditViewModel(service)
            {
                Package = SelectedPackage,
                Employees = service.GetAvailableEmployees(SelectedPackage),
                SelectedEmployee = service.GetAssignedEmployee(SelectedPackage)
            });
            await Task.Run(() =>
            {
                if (result != true)
                {
                    service.Reload(SelectedPackage);
                }
                else
                {
                    service.Save();
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
                    service.RemovePackage(SelectedPackage);
                    UpdateData();
                }
            });
        }
    }
}