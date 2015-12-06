using Caliburn.Micro;
using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    public class ShowCourierPackagesViewModel : CourierPackagesViewModelBase
    {
        private readonly PackagesServiceProxy service;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="service"></param>
        public ShowCourierPackagesViewModel(PackagesServiceProxy service)
            : base(service)
        {
            this.service = service;
        }

    }
}