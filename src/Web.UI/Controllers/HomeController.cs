using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            if(User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
