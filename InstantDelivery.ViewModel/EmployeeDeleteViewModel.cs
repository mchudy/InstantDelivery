using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class EmployeeDeleteViewModel : Screen
    {
        public void Remove()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
