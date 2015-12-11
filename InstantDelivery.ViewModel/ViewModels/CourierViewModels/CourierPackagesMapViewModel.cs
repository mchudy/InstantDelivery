using Caliburn.Micro;
using InstantDelivery.Model;
using InstantDelivery.Model.Packages;
using InstantDelivery.ViewModel.Proxies;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
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
                new PackageDto
                {
                    Id = 100,
                    ShippingAddress = new AddressDto()
                    {
                        City = "Warszawa",
                        Street = "Marszałkowska",
                        Number = "50",
                        PostalCode = "00-500"
                    },
                    Weight = 20,
                    Height = 20,
                    Length = 50,
                    Width = 60
                },
                new PackageDto
                {
                    Id = 101,
                    ShippingAddress = new AddressDto()
                    {
                        City = "Warszawa",
                        Street = "Nowy Świat",
                        Number = "50",
                        PostalCode = "00-500"
                    }
                },
                new PackageDto
                {
                    Id = 104,
                    ShippingAddress = new AddressDto()
                    {
                        City = "Warszawa",
                        Street = "Koszykowa",
                        Number = "50",
                        PostalCode = "00-500"
                    }
                }
            };
        }

        public List<PackageDto> Packages { get; set; }

        public event EventHandler<InvokeScriptEventArgs> InvokeScript;

        public void OnPageLoaded()
        {
            ShowPackages();
        }

        public void ShowRoute()
        {
            InvokeScriptFunction("showRoute");
        }

        public void ShowPackages()
        {
            InvokeScriptFunction("showPackages");
        }

        private void InvokeScriptFunction(string functionName)
        {
            string json = JsonConvert.SerializeObject(Packages, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            InvokeScript?.Invoke(this, new InvokeScriptEventArgs
            {
                FunctionName = functionName,
                Parameters = new[] { (object)json }
            });
        }
    }

    public class InvokeScriptEventArgs : EventArgs
    {
        public string FunctionName { get; set; }
        public object[] Parameters { get; set; }
    }
}