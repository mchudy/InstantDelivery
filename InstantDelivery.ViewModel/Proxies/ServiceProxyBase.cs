using InstantDelivery.ViewModel.Dialogs;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    /// <summary>
    /// Bazowa klasa dla proxy łączących się z Web API
    /// </summary>
    public abstract class ServiceProxyBase
    {
        private readonly string controllerName;
        private readonly IDialogManager dialogManager;

        protected static readonly Uri baseUri = new Uri(ConfigurationManager.AppSettings["ApiAddress"]);
        protected static HttpClient client;

        protected ServiceProxyBase(string controllerName, IDialogManager dialogManager)
        {
            this.controllerName = controllerName;
            this.dialogManager = dialogManager;
        }

        /// <summary>
        /// Wywołuje metodę GET dla podanego adresu
        /// </summary>
        /// <typeparam name="TResult">Oczekiwany typ odpowiedzi</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TResult> Get<TResult>(string query)
        {
            try
            {
                var response = await client.GetAsync($"{controllerName}/{query}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<TResult>();
                }
                await HandleErrors(response);
            }
            catch (HttpRequestException)
            {
                await ShowConnectionError();
            }
            return default(TResult);
        }

        /// <summary>
        /// Wywołuje metodę DELETE
        /// </summary>
        /// <param name="id">Identyfikator zasobu</param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"{controllerName}/{id}");
                await HandleErrors(response);
            }
            catch (HttpRequestException)
            {
                await ShowConnectionError();
            }
        }

        /// <summary>
        /// Wywołuje metodę PUT
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task Put<TRequest>(TRequest dto)
        {
            try
            {
                var response = await client.PutAsJsonAsync(controllerName, dto);
                await HandleErrors(response);
            }
            catch (HttpRequestException)
            {
                await ShowConnectionError();
            }
        }

        /// <summary>
        /// Wywołuje metodę POST pod danym adresem, która nie zwraca odpowiedzi.
        /// Dane przesyłane są jako JSON
        /// </summary>
        /// <typeparam name="TRequest">Typ obiektu, który ma zostać przesłany jako JSON</typeparam>
        /// <param name="query"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task PostAsJson<TRequest>(string query, TRequest dto)
        {
            try
            {
                var response = await client.PostAsJsonAsync($"{controllerName}/{query}", dto);
                await HandleErrors(response);
            }
            catch (HttpRequestException)
            {
                await ShowConnectionError();
            }
        }

        /// <summary>
        /// Wywołuje metodę POST pod danym adresem, która zwraca odpowiedź
        /// Dane przesyłane są jako JSON
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="query"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<TResult> PostAsJson<TRequest, TResult>(string query, TRequest dto)
        {
            try
            {
                var response = await client.PostAsJsonAsync($"{controllerName}/{query}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<TResult>();
                }
                await HandleErrors(response);
            }
            catch (HttpRequestException)
            {
                await ShowConnectionError();
            }
            return default(TResult);
        }

        /// <summary>
        /// Wywołuje metodę POST pod danym adresem, która nie zwraca odpowiedzi
        /// Dane przesyłane są jako form-urlencoded
        /// </summary>
        /// <param name="query"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task Post(string query, HttpContent content)
        {
            try
            {
                var response = await client.PostAsync($"{controllerName}/{query}", content);
                await HandleErrors(response);
            }
            catch (HttpRequestException)
            {
                await ShowConnectionError();
            }
        }

        /// <summary>
        /// Wywołuje metodę POST pod danym adresem, która zwraca odpowiedź
        /// Dane przesyłane są jako form-urlencoded
        /// </summary>
        /// <param name="query"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<TResult> Post<TResult>(string query, HttpContent content)
        {
            try
            {
                var response = await client.PostAsync($"{controllerName}/{query}", content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<TResult>();
                }
                await HandleErrors(response);
            }
            catch (HttpRequestException)
            {
                await ShowConnectionError();
            }
            return default(TResult);
        }

        private async Task HandleErrors(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                await ShowError(response.StatusCode);
            }
        }

        private async Task ShowConnectionError()
        {
            await dialogManager.ShowDialogAsync(new ErrorDialogViewModel
            {
                Title = "Błąd połączenia",
                Message =
                    "Nie można połączyć się z serwerem. Upewnij się, że komputer jest połączony z Internetem.",
            });
        }

        private async Task ShowError(HttpStatusCode statusCode)
        {
            string title = "Błąd";
            string message;
            if (statusCode == HttpStatusCode.NotFound)
            {
                message = "Nie znaleziono danego obiektu. Może to oznaczać, że został usunięty przez innego pracownika.";
            }
            else if (statusCode == HttpStatusCode.BadRequest)
            {
                message = "Upewnij się, czy wprowadzone zostały poprawne dane";
            }
            else
            {
                message = "Wystąpił błąd. Spróbuj ponownie za chwilę.";
            }
            await dialogManager.ShowDialogAsync(new ErrorDialogViewModel
            {
                Title = title,
                Message = message
            });
        }
    }
}
