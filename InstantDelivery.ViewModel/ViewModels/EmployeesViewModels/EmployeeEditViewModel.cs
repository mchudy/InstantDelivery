using Caliburn.Micro;
using InstantDelivery.Model;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku edycji pracownika
    /// </summary>
    public class EmployeeEditViewModel : Screen
    {
        /// <summary>
        /// Aktualnie edytowany pracownik.
        /// </summary>
        public EmployeeDto Employee { get; set; }

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
