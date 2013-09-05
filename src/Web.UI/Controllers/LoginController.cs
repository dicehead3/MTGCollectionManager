using System;
using System.Web.Mvc;
using System.Web.Security;
using Domain;
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
                var v = User.Identity.IsAuthenticated;
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
            if (ModelState.IsValid)
            {
                var user = new User(model.Email, model.DisplayName, _userRepository);
                _userRepository.CreateNewUser(user, model.Password);
                if (_userRepository.AuthenticateUser(model.Email, model.Password))
                {
                    var v = User.Identity.IsAuthenticated;
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
