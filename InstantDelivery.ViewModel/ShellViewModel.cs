using Caliburn.Micro;
using InstantDelivery.Core.Services;

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
            ActivateItem(new EmployeesViewModel(new EmployeesRepository()));
        }

        public void Vehicles()
        {
            ActivateItem(new VehiclesViewModel());
        }
    }
}
