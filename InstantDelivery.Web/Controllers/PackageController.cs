using InstantDelivery.Model.Packages;
using InstantDelivery.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InstantDelivery.Web.Controllers
{
    /// <summary>
    /// Kontroler zwracający widoki związane z paczkami
    /// </summary>
    public class PackageController : Controller
    {
        private const string baseUri = "https://instantdelivery.azurewebsites.net/api/";
        private readonly HttpClient client = new HttpClient();

        public PackageController()
        {
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Pokazuje historię zdarzeń związanych z daną paczką
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Track(int packageId)
        {
            ViewBag.PackageId = packageId;
            var response = await client.GetAsync($"packages/{packageId}/history");
            if (response.IsSuccessStatusCode)
            {
                var dtos = await response.Content.ReadAsAsync<IEnumerable<PackageEventDto>>();
                return View(dtos);
            }
            return View();
        }

        /// <summary>
        /// Wyświetla formularz do obliczenia kosztu paczki
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Cost()
        {
            return View();
        }

        /// <summary>
        /// Wyświetla wynik obliczenia kosztu paczki
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Cost(PackageCostModel model)
        {
            if (ModelState.IsValid)
            {
                string query = BuildQueryString(model);
                var response = await client.GetAsync($"packages/cost?{query}");
                if (response.IsSuccessStatusCode)
                {
                    var cost = await response.Content.ReadAsAsync<decimal>();
                    ViewBag.Cost = cost;
                    return View(model);
                }
            }
            return View();
        }

        private string BuildQueryString(PackageCostModel model)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString[nameof(PackageDto.Height)] = model.Height.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Width)] = model.Width.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Length)] = model.Length.ToString(CultureInfo.InvariantCulture);
            queryString[nameof(PackageDto.Weight)] = model.Weight.ToString(CultureInfo.InvariantCulture);
            return queryString.ToString();
        }
    }
}