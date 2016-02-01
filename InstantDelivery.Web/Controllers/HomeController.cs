using System.Web.Mvc;

namespace InstantDelivery.Web.Controllers
{
    /// <summary>
    /// Główny kontroler aplikacji
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Strona główna
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Strona "O nas"
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }
    }
}