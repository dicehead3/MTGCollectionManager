using System.Web.Mvc;
using Domain.AbstractRepositories;

namespace Web.UI.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IDeckRepository _deckRepository;

        public CollectionController(IUserRepository userRepository, ICardRepository cardRepository, IDeckRepository deckRepository)
        {
            _userRepository = userRepository;
            _cardRepository = cardRepository;
            _deckRepository = deckRepository;
        }

        public ActionResult CollectionManager()
        {
            return View();
        }

        public ActionResult CollectionOverview()
        {
            return View();
        }

        public ActionResult DeckCreator()
        {
            return View();
        }

        public ActionResult DeckOverview()
        {
            return View();
        }
    }
}
