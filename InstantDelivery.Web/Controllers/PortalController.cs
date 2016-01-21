using System.Web.Mvc;

namespace InstantDelivery.Web.Controllers
{
    public class PortalController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        public ActionResult MyPackages()
        {
            return PartialView();
        }

        public ActionResult SendPackage()
        {
            return PartialView();
        }
    }
}