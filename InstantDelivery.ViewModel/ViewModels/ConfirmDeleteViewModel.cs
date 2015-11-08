using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku potwierdzenia usuwania obiektu.
    /// </summary>
    public class ConfirmDeleteViewModel : Screen
    {
        /// <summary>
        /// Potwierdza chęć dokonania zmian.
        /// </summary>
        public void Remove()
        {
            TryClose(true);
        }

        /// <summary>
        /// Anuluje bieżący proces dokonywania zmian.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }
    }
}
