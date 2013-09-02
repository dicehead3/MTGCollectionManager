using System.Linq;
using Data.Repositories;
using Domain;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Integration.Data
{
    public class UserRepositoryTests : DataTestFixture
    {
        [Test]
        public void CanCreateUser()
        {
            var userRepository = new UserRepository(Session);
            var cardRepository = new CardRepository(Session);
            var deckRepository = new DeckRepository(Session);
            
            var user = new User("Aap", "Noot", userRepository);
            var card = new Card("A", "B", "C", CardRarity.Special, "D", "E");
            var deck = new Deck("F");
            deck.CardInDeck.Add(card);
            user.Cards.Add(card);
            user.Decks.Add(deck);

            Session.Transaction.Begin();
            cardRepository.Save(card);
            deckRepository.Save(deck);
            userRepository.Save(user);
            Session.Transaction.Commit();

            Session.Evict(card);
            Session.Evict(deck);
            Session.Evict(user);
            Session.Clear();

            var userFromRepository = userRepository.Get(user.Id);

            //Assert.AreEqual(userFromRepository.Name, user.Name);
            Assert.AreEqual(userFromRepository.Cards.Count, 1);
            Assert.AreEqual(userFromRepository.Decks.First().Name, deck.Name);
            Assert.AreEqual(userFromRepository.Decks.First().CardInDeck.First().Name, card.Name);
        }
    }
}
