using System;
using System.Web.Mvc;

namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            var random = new Random();
            switch (random.Next(0, 5))
            {
                case 0:
                    ViewData["logo"] = "W";
                    break;
                case 1:
                    ViewData["logo"] = "U";
                    break;
                case 2:
                    ViewData["logo"] = "B";
                    break;
                case 3:
                    ViewData["logo"] = "R";
                    break;
                case 4:
                    ViewData["logo"] = "G";
                    break;
            }

            ViewBag.Title = "Home - Index";
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
