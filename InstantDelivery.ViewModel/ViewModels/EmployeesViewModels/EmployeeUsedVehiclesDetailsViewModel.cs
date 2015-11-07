using Caliburn.Micro;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.ViewModel
{
    public class EmployeeUsedVehiclesDetailsViewModel : Screen
    {
        public Employee Employee { get; set; }

        public void CloseWindow()
        {
            TryClose(false);
        }
    }
}