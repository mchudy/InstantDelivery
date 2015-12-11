using Caliburn.Micro;
using InstantDelivery.Model.Packages;
using InstantDelivery.Model.Paging;
using InstantDelivery.ViewModel.Proxies;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel
{
    public class CourierPackagesMapViewModel : Screen
    {
        private readonly PackagesServiceProxy service;

        public CourierPackagesMapViewModel(PackagesServiceProxy service)
        {
            this.service = service;
        }

        public IList<PackageDto> Packages { get; set; }

        public event EventHandler<InvokeScriptEventArgs> InvokeScript;

        public async void OnPageLoaded()
        {
            Packages = await LoadPackages();
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

        private async Task<IList<PackageDto>> LoadPackages()
        {
            var result = await service.PageForLoggedEmployee(new PageQuery
            {
                PageSize = 100,
                PageIndex = 1
            });
            return result?.PageCollection;
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