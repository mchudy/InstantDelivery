using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class LoginServiceProxy
    {
        private HttpClient client = new HttpClient();

        public LoginServiceProxy()
        {
            client.BaseAddress = new Uri("http://localhost:13600/api/Login/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static string SecureStringToString(SecureString value)
        {
            var bstr = Marshal.SecureStringToBSTR(value);

            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }

        public async Task Login(string username, SecureString password)
        {
            var response = await client.PostAsJsonAsync("Login",new [] {username, SecureStringToString(password)});
            response.EnsureSuccessStatusCode();
        }

        public async Task SendPassword(string email)
        {
            var response = await client.PostAsJsonAsync("SendPassword",email);
            response.EnsureSuccessStatusCode();
        }
    }
}