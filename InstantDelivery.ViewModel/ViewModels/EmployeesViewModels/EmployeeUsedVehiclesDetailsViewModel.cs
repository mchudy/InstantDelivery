using Caliburn.Micro;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku szczegółów używanego pojazdu przez pacownika.
    /// </summary>
    public class EmployeeUsedVehiclesDetailsViewModel : Screen
    {
        /// <summary>
        /// Pracownik którego szczegóły są wyświetlane na widoku.
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Delegat zdarzenia zamykania okna. Widok jest tylko do odczytu więc jest to tylko zamknięcie okna.
        /// </summary>
        public void CloseWindow()
        {
            TryClose(false);
        }
    }
}