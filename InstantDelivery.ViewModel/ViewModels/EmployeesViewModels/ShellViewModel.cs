using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        public ShellViewModel()
        {
            DisplayName = "Instant Delivery";
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

        public void EmployeeContactInformations()
        {
            ActivateItem(IoC.Get<EmployeeContactInformationsViewModel>());
        }
        public void EmployeeIdentitingInformations()
        {
            ActivateItem(IoC.Get<EmployeeIdentitingInformationsViewModel>());
        }
        public void EmployeePlacesOfResidence()
        {
            ActivateItem(IoC.Get<EmployeePlacesOfResidenceViewModel>());
        }

        public void VehiclesGeneral()
        {
            ActivateItem(IoC.Get<VehiclesGeneralViewModel>());
        }
    }
}
