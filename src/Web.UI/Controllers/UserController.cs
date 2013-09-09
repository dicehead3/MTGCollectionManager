using System.Web.Mvc;
using Domain;
using Domain.AbstractRepositories;
using Web.UI.ViewModels;

namespace Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model, string returnUrl)
        {
            var result = _userRepository.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword,
                model.ConfirmPassword);

            if (result == ChangePassswordMessage.PasswordChanged)
            {
                if (Url.IsLocalUrl(returnUrl) && returnUrl != "/")
                    Redirect(returnUrl);
                return RedirectToAction("CollectionManager", "Collection");
            }
            else
            {
                //Show errors
            }
            return View();
        }
    }
}
