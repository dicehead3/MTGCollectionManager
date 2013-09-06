using System.Collections.Generic;
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
        private readonly IUserRepository _userRepository;

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
            var authenticationResult = _userRepository.AuthenticateUser(model.Email, model.Password);
            if (authenticationResult == AuthenticateMessages.AuthenticationSuccessfull)
            {
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl) && returnUrl != "/")
                {
                    Redirect(returnUrl);
                }
                return RedirectToAction("CollectionManager", "Collection");
            }
            if (authenticationResult == AuthenticateMessages.UsernameDoesNotExist)
            {
                //Error message Unknown username
            }
            else
            {
                //Error message Email or password is incorrect
            }
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
            if (!ModelState.IsValid) 
                return View();
            var user = new User(model.Email, model.DisplayName, _userRepository);
            _userRepository.CreateNewUser(user, model.Password);
            var authenticationResult = _userRepository.AuthenticateUser(model.Email, model.Password);
            if (authenticationResult == AuthenticateMessages.AuthenticationSuccessfull)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("CollectionManager", "Collection");
            }
                return RedirectToAction("Index", "Home");
        }
    }
}
