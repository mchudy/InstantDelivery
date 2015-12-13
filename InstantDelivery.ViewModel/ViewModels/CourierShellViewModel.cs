using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Model widoku głównego okna kuriera
    /// </summary>
    public class CourierShellViewModel : Conductor<object>.Collection.OneActive
    {
        public CourierShellViewModel()
        {
            ActivateItem(IoC.Get<StartViewModel>());
        }

        /// <summary>
        /// Wyświetla widok z danymi osobowymi kuriera
        /// </summary>
        public void ShowCourierProfile()
        {
            ActivateItem(IoC.Get<CourierProfileViewModel>());
        }

        /// <summary>
        /// Wyświetla widok z przesyłkami kuriera
        /// </summary>
        public void ShowCourierPackages()
        {
            ActivateItem(IoC.Get<CourierPackagesViewModel>());
        }

        /// <summary>
        /// Wyświetla widok mapy przesyłek
        /// </summary>
        public void DisplayMapOfCourierPackages()
        {
            ActivateItem(IoC.Get<CourierPackagesMapViewModel>());
        }

        /// <summary>
        /// Wyświetla widok zmiany hasła
        /// </summary>
        public void ChangePassword()
        {
            ActivateItem(IoC.Get<ChangePasswordViewModel>());
        }
    }
}