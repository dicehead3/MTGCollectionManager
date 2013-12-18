using System.Web.Mvc;

namespace UI.Web.Controllers.Base
{
    [Authorize]
    public abstract class AuthorizedController : BaseController
    {
    }
}
