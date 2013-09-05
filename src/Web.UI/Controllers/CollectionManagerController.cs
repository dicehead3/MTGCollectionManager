using System.Web.Mvc;

namespace Web.UI.Controllers
{
    [Authorize]
    public class CollectionManagerController : Controller
    {
        //
        // GET: /CollectionManager/

        public ActionResult Index()
        {
            return View();
        }

    }
}
