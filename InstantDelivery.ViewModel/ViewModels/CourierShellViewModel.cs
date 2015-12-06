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
            ActivateItem(IoC.Get<ShowCourierProfileViewModel>());
        }

        public void ShowCourierPackages()
        {
            ActivateItem(IoC.Get<ShowCourierPackagesViewModel>());
        }

        public void DisplayMapOfCourierPackages()
        {
            ActivateItem(IoC.Get<DisplayMapOfCourierPackagesViewModel>());
        }
    }
}