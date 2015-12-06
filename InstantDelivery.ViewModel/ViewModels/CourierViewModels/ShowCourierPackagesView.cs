using Caliburn.Micro;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    public class ShowCourierPackagesView : PackagesViewModelBase
    {
        // somehow load current employee's packages
        private readonly PackagesServiceProxy service;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="service"></param>
        public ShowCourierPackagesView(PackagesServiceProxy service)
            : base(service)
        {
            this.service = service;
        }

    }
}