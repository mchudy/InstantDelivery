using System.Web.Mvc;

namespace InstantDelivery.Web.Controllers
{
    public class PortalController : Controller
    {
        /// <summary>
        /// Punkt wejścia dla aplikacji SPA (portalu dla użytkowników)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}