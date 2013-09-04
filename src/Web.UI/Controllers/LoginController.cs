using System.Web.Mvc;
using Web.UI.Models;

namespace Web.UI.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model, string returnUrl)
        {
            return View();
        }
    }
}
