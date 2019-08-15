using MVC.Legacy.Attributes;
using MVC.Legacy.Utils;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC.Legacy.Controllers
{
    public class HomeController : Controller
    {
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

        [Authorize]
        public void GoIframe()
        {
            var domain = System.Configuration.ConfigurationManager.AppSettings["MVC.Legacy.Domain"];
            var path = "/";

            var ticketSSO = new HttpCookie("TicketSSO", TicketSSO.GetCookie());
            ticketSSO.Domain = domain;
            ticketSSO.Path = path;

            HttpContext.Response.SetCookie(ticketSSO);

            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["MVC.Legacy.UrlRedirect"]);
        }

        [AllowCrossSiteAttribute]
        public ActionResult SessionValidation()
        {
            bool isAutth = HttpContext.User.Identity.IsAuthenticated;

            return isAutth ? new HttpStatusCodeResult(HttpStatusCode.OK) : new HttpUnauthorizedResult("Sin accesos");
        }
    }
}