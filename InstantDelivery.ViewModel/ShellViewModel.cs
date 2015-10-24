using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        public void Employees()
        {
            ActivateItem(new EmployeesViewModel());
        }

        public void Vehicles()
        {
            ActivateItem(new VehiclesViewModel());
        }
    }
}
