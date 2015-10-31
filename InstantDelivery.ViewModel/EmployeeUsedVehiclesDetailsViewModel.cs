using Caliburn.Micro;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.ViewModel
{
    public class EmployeeUsedVehiclesDetailsViewModel : Screen
    {
        public Employee SelectedRow { get; set; }


        public void CloseWindow()
        {
            TryClose(false);
        }
    }
}