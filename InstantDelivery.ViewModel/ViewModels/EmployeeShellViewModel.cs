using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class EmployeeShellViewModel : Conductor<object>.Collection.OneActive
    {
        public EmployeeShellViewModel()
        {
            ActivateItem(IoC.Get<StartViewModel>());
        }

        /// <summary>
        /// Widok pracowników
        /// </summary>
        public void Employees()
        {
            ActivateItem(IoC.Get<EmployeesViewModel>());
        }

        /// <summary>
        /// Widok dodawania pracownika
        /// </summary>
        public void EmployeeAdd()
        {
            ActivateItem(IoC.Get<EmployeeAddViewModel>());
        }

        /// <summary>
        /// Widok przeglądania paczek pracowników
        /// </summary>
        public void EmployeesManagedPackages()
        {
            ActivateItem(IoC.Get<EmployeesManagedPackagesViewModel>());
        }

        /// <summary>
        /// Widok przeglądania samochodów pracowników
        /// </summary>
        public void EmployeesUsedVehicles()
        {
            ActivateItem(IoC.Get<EmployeesUsedVehiclesViewModel>());
        }

        /// <summary>
        /// Ogólny widok pojazdów
        /// </summary>
        public void VehiclesGeneral()
        {
            ActivateItem(IoC.Get<VehiclesGeneralViewModel>());
        }

        /// <summary>
        /// Widok dodawania pojazdu
        /// </summary>
        public void VehiclesAdd()
        {
            ActivateItem(IoC.Get<VehiclesAddViewModel>());
        }

        /// <summary>
        /// Widok statystyk dostarczonych paczek
        /// </summary>
        public void StatisticsDeliveredPackages()
        {
            ActivateItem(IoC.Get<StatisticsDeliveredPackagesViewModel>());
        }

        /// <summary>
        /// Widok statystyk pojazdów
        /// </summary>
        public void StatisticsEmployeesVehicles()
        {
            ActivateItem(IoC.Get<StatisticsEmployeesVehiclesViewModel>());
        }

        /// <summary>
        /// Ogólny widok paczek
        /// </summary>
        public void GeneralPackages()
        {
            ActivateItem(IoC.Get<GeneralPackagesViewModel>());
        }

        /// <summary>
        /// Widok dodawania paczki
        /// </summary>
        public void PackageAdd()
        {
            ActivateItem(IoC.Get<PackageAddViewModel>());
        }

        /// <summary>
        /// Widok zarządzania pojazdami
        /// </summary>
        public void VehicleManage()
        {
            ActivateItem(IoC.Get<VehicleManageViewModel>());
        }
    }
}
