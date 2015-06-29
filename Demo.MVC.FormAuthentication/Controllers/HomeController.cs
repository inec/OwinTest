using System.Web.Mvc;

namespace Demo.MVC.FormAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {

            //@ViewBag.acc = LoginViewModel;
            return View();
        }

        public ActionResult Contact()
        {

            //@ViewBag.acc = LoginViewModel;
            //AccountController
            // return RedirectToAction("login");
            return View();
            //Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);


        }
    }
}