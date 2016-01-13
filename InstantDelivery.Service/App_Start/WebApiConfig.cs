using InstantDelivery.Service.Filters;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace InstantDelivery.Service
{
    /// <summary>
    /// Konfiguruje web api
    /// </summary>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // return JSON instead of XML as default
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes
                .FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // use camelCase in JSON
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // ReSharper disable once UnusedVariable
            var requireHttpsAttribute = new RequireHttpsAttribute();

            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
#if !DEBUG
            config.Filters.Add(requireHttpsAttribute);
#endif
        }
    }
}
