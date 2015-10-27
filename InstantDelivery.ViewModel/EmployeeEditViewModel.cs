using Caliburn.Micro;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.ViewModel
{
    public class EmployeeEditViewModel : Screen
    {
        public Employee Employee { get; set; }

        public void Save()
        {

        }

        public void Cancel()
        {
            TryClose();
        }
    }
}
