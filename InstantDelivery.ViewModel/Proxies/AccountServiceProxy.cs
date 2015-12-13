using InstantDelivery.Common.Enums;
using InstantDelivery.Model;
using InstantDelivery.ViewModel.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class AccountServiceProxy : ServiceProxyBase
    {
        private readonly IDialogManager dialogManager;

        public AccountServiceProxy(IDialogManager dialogManager)
            : base("Account", dialogManager)
        {
            this.dialogManager = dialogManager;
        }

        /// <summary>
        /// Zwraca role danego pracownika.
        /// </summary>
        /// <returns></returns>
        public async Task<Role[]> GetRoles()
        {
            return await Get<Role[]>("Roles");
        }

        /// <summary>
        /// Zmienia hasło danego użutkownika.
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <returns></returns>
        public async Task ChangePassword(ChangePasswordDto changePasswordDto)
        {
            await PostAsJson("ChangePassword", changePasswordDto);
        }

        /// <summary>
        /// Loguje użytkownika.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {
            var response = GetToken(username, password);
            try
            {
                dynamic responseJson = JObject.Parse(response);
                string token = responseJson["access_token"];
                if (token != null)
                {
                    client = CreateClient(token);
                    return true;
                }
            }
            catch (JsonReaderException)
            {
                dialogManager.ShowDialogAsync(new ErrorDialogViewModel
                {
                    Title = "Błąd",
                    Message = "Wystąpił błąd podczas logowania. Spróbuj ponownie za chwilę."
                }).ConfigureAwait(false);
            }
            return false;
        }

        /// <summary>
        /// Wwylogowuje użytkownika.
        /// </summary>
        public void Logout()
        {
            client?.CancelPendingRequests();
            client?.Dispose();
            client = null;
        }

        private static string GetToken(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("grant_type", "password"),
                            new KeyValuePair<string, string>("username", userName ),
                            new KeyValuePair<string, string>("password", password )
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(baseUri + "Token", content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        private static HttpClient CreateClient(string accessToken)
        {
            var newClient = new HttpClient();
            newClient.BaseAddress = baseUri;
            newClient.DefaultRequestHeaders.Accept.Clear();
            newClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(accessToken))
            {
                newClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
            return newClient;
        }
    }
}
