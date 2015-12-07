using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.Model.Vehicles;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    ///  Model widoku edycji pojazdu.
    /// </summary>
    public class VehicleEditViewModel : Screen
    {
        /// <summary>
        /// Edytowany pojazd w widoku.
        /// </summary>
        public VehicleDto Vehicle { get; set; }

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