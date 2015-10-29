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
    }
}
