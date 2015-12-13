using InstantDelivery.Service.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace InstantDelivery.Service
{
    /// <summary>
    /// Klasa konfigurująca autoryzację
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Ustawienia autoryzacji
        /// </summary>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        /// <summary>
        /// ID klienta
        /// </summary>
        public static string PublicClientId { get; private set; }

        /// <summary>
        /// Konfiguruje autoryzację
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            PublicClientId = "self";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
#if DEBUG
                AllowInsecureHttp = true
#endif
            };

            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
