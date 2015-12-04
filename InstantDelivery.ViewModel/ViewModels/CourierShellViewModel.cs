using Caliburn.Micro;

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
            //TODO
        }

        public void ShowCourierPackages()
        {
            //TODO
        }

        public void DisplayMapOfCourierPackages()
        {
            //TODO
        }
    }
}