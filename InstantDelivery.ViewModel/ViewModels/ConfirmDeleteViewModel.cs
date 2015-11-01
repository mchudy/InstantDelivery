using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class ConfirmDeleteViewModel : Screen
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
