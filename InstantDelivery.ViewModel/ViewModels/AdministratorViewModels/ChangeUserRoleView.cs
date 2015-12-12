using Caliburn.Micro;
using InstantDelivery.Model;

namespace InstantDelivery.ViewModel
{
    public class ChangeUserRoleViewModel : Screen
    {
        /// <summary>
        /// Aktualnie edytowany użytkownik.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Zapisuje zmiany dokonane w widoku.
        /// </summary>
        public void Save()
        {
            TryClose(true);
        }

        /// <summary>
        /// Anuluje zmiany dokonane w widoku.
        /// </summary>
        public void Cancel()
        {
            TryClose(false);
        }

    }
}
