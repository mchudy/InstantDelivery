using Caliburn.Micro;
using InstantDelivery.ViewModel.ViewModels;

namespace InstantDelivery.ViewModel
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        public ShellViewModel()
        {
            DisplayName = "Instant Delivery";
            ActivateItem(IoC.Get<StartViewModel>());
        }

        public void Employees()
        {
            //TODO
            ActivateItem(IoC.Get<EmployeesViewModel>());
        }

        public void EmployeeAdd()
        {
            ActivateItem(IoC.Get<EmployeeAddViewModel>());
        }

        public void EmployeesManagedPackages()
        {
            ActivateItem(IoC.Get<EmployeesManagedPackagesViewModel>());
        }

        public void EmployeesUsedVehicles()
        {
            ActivateItem(IoC.Get<EmployeesUsedVehiclesViewModel>());
        }

        public void VehiclesGeneral()
        {
            ActivateItem(IoC.Get<VehiclesGeneralViewModel>());
        }

        public void VehiclesAdd()
        {
            ActivateItem(IoC.Get<VehiclesAddViewModel>());
        }

        public void StatisticsDeliveredPackages()
        {
            ActivateItem(IoC.Get<StatisticsDeliveredPackagesViewModel>());
        }

        public void StatisticsEmployeesVehicles()
        {
            ActivateItem(IoC.Get<StatisticsEmployeesVehiclesViewModel>());
        }

        public void GeneralPackages()
        {
            ActivateItem(IoC.Get<GeneralPackagesViewModel>());
        }

        public void PackageAdd()
        {
            ActivateItem(IoC.Get<PackageAddViewModel>());
        }

        public void VehicleManage()
        {
            ActivateItem(IoC.Get<VehicleManageViewModel>());

        }
    }
}
