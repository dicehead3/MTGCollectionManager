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
        public void CanRegisterUser()
        {
            var userRepository = new UserRepository(Session, Encryptor);
            var cardRepository = new CardRepository(Session);
            var deckRepository = new DeckRepository(Session);

            var card = new Card("A", "B", "C", CardRarity.Special, "D", "E");
            var deck = new Deck("F");
            var user = new User("aap@noot.mies", "Appie", userRepository);
            deck.Cards.Add(card);
            deck.Cards.Add(card);
            user.Decks.Add(deck);
            user.Cards.Add(card);

            Session.BeginTransaction();

            userRepository.CreateNewUser(user, "test123");
            cardRepository.Save(card);
            deckRepository.Save(deck);

            Session.Transaction.Commit();

            Session.Evict(card);
            Session.Evict(deck);
            Session.Evict(user);
            Session.Clear();

            var userFromRepository = userRepository.Get(user.Id);

            Assert.AreEqual(user.DisplayName, userFromRepository.DisplayName);
            Assert.AreEqual(userFromRepository.Decks[0].Cards.Count, deck.Cards.Count);
            Assert.AreEqual(userFromRepository.Cards.Count, 1);
            Assert.IsTrue(userRepository.AuthenticateUser(userFromRepository.Email, "test123"));
        }

        [Test]
        public void UserCanChangePassword()
        {
            var userRepository = new UserRepository(Session, Encryptor);
            var cardRepository = new CardRepository(Session);
            var deckRepository = new DeckRepository(Session);

            var card = new Card("A", "B", "C", CardRarity.Special, "D", "E");
            var deck = new Deck("F");
            var user = new User("aap@noot.mies", "Appie", userRepository);
            deck.Cards.Add(card);
            deck.Cards.Add(card);
            user.Decks.Add(deck);
            user.Cards.Add(card);

            Session.BeginTransaction();

            userRepository.CreateNewUser(user, "test123");
            cardRepository.Save(card);
            deckRepository.Save(deck);

            Session.Transaction.Commit();

            Session.Evict(card);
            Session.Evict(deck);
            Session.Evict(user);
            Session.Clear();

            var userFromRepository = userRepository.Get(user.Id);

            var message = userRepository.ChangePassword(userFromRepository.Email, "test123", "etende", "etende");

            Assert.AreEqual(message, ChangePassswordMessage.PasswordChanged);
        }
    }
}
