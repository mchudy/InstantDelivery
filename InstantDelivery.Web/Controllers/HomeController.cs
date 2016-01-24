using InstantDelivery.Model.Packages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InstantDelivery.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string baseUri = "https://instantdelivery.azurewebsites.net/api/";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Track(int packageId)
        {
            ViewBag.PackageId = packageId;
            //TODO: wrapper and probably seperate controller
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"packages/{packageId}/history");
                if (response.IsSuccessStatusCode)
                {
                    var dtos = await response.Content.ReadAsAsync<IEnumerable<PackageEventDto>>();
                    return View(dtos);
                }
            }
            //TODO: dedicated error page
            return View();
        }

        public ActionResult PackageCost()
        {
            //TODO
            return null;// View();
        }
    }
}