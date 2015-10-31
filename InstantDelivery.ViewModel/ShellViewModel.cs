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

        public void Vehicles()
        {
            ActivateItem(new VehiclesViewModel());
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
    }
}
