using System.Web.Mvc;
using System.Web.Security;
using Domain.AbstractRepositories;
using Web.UI.Models;
using Web.UI.ViewModels;

namespace Web.UI.Controllers
{
    public class LoginController : Controller
    {
        private IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model, string returnUrl)
        {
            if (_userRepository.AuthenticateUser(model.Email, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl) && returnUrl != "/")
                {
                    Redirect(returnUrl);
                }
                return RedirectToAction("Index", "CollectionManager");
            }
            ModelState.AddModelError("", "Email or password is incorrect");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Title = "Register";
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            return View();
        }
    }
}
