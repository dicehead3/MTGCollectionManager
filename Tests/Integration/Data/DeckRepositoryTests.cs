using Data.Repositories;
using Domain;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Integration.Data
{
    public class DeckRepositoryTests : DataTestFixture
    {
        [Test]
        public void CanCreateDeck()
        {
            var deckRepository = new DeckRepository(Session);
            var cardRepository = new CardRepository(Session);
            var card = new Card("A", "type", "m14", CardRarity.Special, "Barry", "nowhere");
            var deck = new Deck("DeckName");
            deck.Cards.Add(card);

            Session.BeginTransaction();
            cardRepository.Save(card);
            deckRepository.Save(deck);
            Session.Transaction.Commit();

            Session.Evict(card);
            Session.Evict(deck);
            Session.Clear();

            var deckFromRepository = deckRepository.Get(deck.Id);

            Assert.AreEqual("DeckName", deckFromRepository.Name);
            Assert.AreEqual(1, deckFromRepository.Cards.Count);
        }
    }
}
