using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.Model.Packages;
using InstantDelivery.ViewModel.Proxies;
using System.Collections.Generic;

namespace InstantDelivery.ViewModel
{
    public class CourierPackagesMapViewModel : Screen
    {
        private readonly PackagesServiceProxy service;

        public CourierPackagesMapViewModel(PackagesServiceProxy service)
        {
            this.service = service;
            this.Packages = new List<PackageDto>
            {
                new PackageDto()
                {
                    Id = 100,
                    ShippingAddress = new AddressDto()
                    {
                        City = "Warszawa",
                        Street = "Marszałkowska",
                        Number = "50"
                    }
                },
                                new PackageDto()
                {
                    Id = 101,
                    ShippingAddress = new AddressDto()
                    {
                        City = "Warszawa",
                        Street = "Nowy Świat",
                        Number = "50"
                    }
                }
            };
        }

        public List<PackageDto> Packages { get; set; }
    }
}