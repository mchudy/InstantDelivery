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
    /// <summary>
    /// Model widoku wyświetlającego mapę z paczkami zalogowanego kuriera
    /// </summary>
    public class CourierPackagesMapViewModel : Screen
    {
        private readonly PackagesServiceProxy service;

        public CourierPackagesMapViewModel(PackagesServiceProxy service)
        {
            this.service = service;
        }

        /// <summary>
        /// Lista paczek kuriera
        /// </summary>
        public IList<PackageDto> Packages { get; set; }

        /// <summary>
        /// Zdarzenie uruchamiane, gdy konieczne jest wywołanie metody
        /// po stronie JavaScriptu w zagnieżdżonej przeglądarce
        /// </summary>
        public event EventHandler<InvokeScriptEventArgs> InvokeScript;

        /// <summary>
        /// Metoda wywoływana, gdy strona w przeglądarce kończy się ładować
        /// </summary>
        public async void OnPageLoaded()
        {
            Packages = await LoadPackages();
            ShowPackages();
        }

        /// <summary>
        /// Wyświetla trasę
        /// </summary>
        public void ShowRoute()
        {
            InvokeScriptFunction("showRoute");
        }

        /// <summary>
        /// Wyświetla paczki na mapie
        /// </summary>
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

    /// <summary>
    /// Zawiera informację konieczne do wywołania skryptu po stronie 
    /// przeglądarki
    /// </summary>
    public class InvokeScriptEventArgs : EventArgs
    {
        /// <summary>
        /// Nazwa funkcji
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// Parametry przekazywane do funkcji
        /// </summary>
        public object[] Parameters { get; set; }
    }
}