using Caliburn.Micro;
using InstantDelivery.ViewModel;

namespace InstantDelivery.ViewModel
{
    public class CourierShellViewModel : Conductor<object>.Collection.OneActive
    {
        public CourierShellViewModel()
        {
            ActivateItem(IoC.Get<StartViewModel>());
        }
        public void ShowCourierProfile()
        {
            ActivateItem(IoC.Get<CourierProfileViewModel>());
        }

        public void ShowCourierPackages()
        {
            ActivateItem(IoC.Get<CourierPackagesViewModel>());
        }

        public void DisplayMapOfCourierPackages()
        {
            ActivateItem(IoC.Get<CourierPackagesMapViewModel>());
        }

        /// <summary>
        /// Widok zmiany hasła
        /// </summary>
        public void ChangePassword()
        {
            ActivateItem(IoC.Get<ChangePasswordViewModel>());
        }
    }
}