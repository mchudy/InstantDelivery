using InstantDelivery.ViewModel.Proxies;

namespace InstantDelivery.ViewModel
{
    public class CourierPackagesViewModel : CourierPackagesViewModelBase
    {
        private readonly PackagesServiceProxy service;

        /// <summary>
        /// Konstruktor modelu widoku
        /// </summary>
        /// <param name="service"></param>
        public CourierPackagesViewModel(PackagesServiceProxy service)
            : base(service)
        {
            this.service = service;
        }

    }
}