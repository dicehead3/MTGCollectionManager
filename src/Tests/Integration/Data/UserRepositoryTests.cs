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
            Assert.AreEqual(userRepository.AuthenticateUser(userFromRepository.Email, "test123"), AuthenticateMessages.AuthenticationSuccessfull);
            Assert.AreEqual(userRepository.AuthenticateUser("jan@post.nu", "blaat"), AuthenticateMessages.UsernameDoesNotExist);
            Assert.AreEqual(userRepository.AuthenticateUser(userFromRepository.Email, "tester"), AuthenticateMessages.WrongEmailOrPassword);
        }

        [Test]

        public void CanAddCardToMultipleUsers()
        {
            var userRepository = new UserRepository(Session, Encryptor);
            var cardRepository = new CardRepository(Session);

            var card = new Card("CardName", "CardType", "Expansion", CardRarity.Mythic, "Artist", "someUrl");

            var user1 = new User("robin@skaele.nl", "Robin van der Knaap", userRepository);
            user1.Cards.Add(card);

            var user2 = new User("erwinb@basecone.nl", "Erwin Bonnet", userRepository);
            user2.Cards.Add(card);

            Session.BeginTransaction();

            cardRepository.Save(card);

            userRepository.CreateNewUser(user1, "test123");
            userRepository.CreateNewUser(user2, "test123");

            Session.Transaction.Commit();

            Session.Evict(card);
            Session.Evict(user1);
            Session.Evict(user2);
            Session.Clear();

            var userFromDatabase1 = userRepository.Get(user1.Id);
            var userFromDatabase2 = userRepository.Get(user2.Id);

            Assert.AreEqual(1, userFromDatabase1.Cards.Count);
            Assert.AreEqual(1, userFromDatabase2.Cards.Count);
            Assert.AreEqual(card, userFromDatabase1.Cards.First());
            Assert.AreEqual(card, userFromDatabase2.Cards.First());
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

        [Test]
        public void CanAddDeckToMultipleUsers()
        {
            var userRepository = new UserRepository(Session, Encryptor);
            var deckRepository = new DeckRepository(Session);

            var deck = new Deck("DeckName");

            var user1 = new User("robin@skaele.nl", "Robin van der Knaap", userRepository);
            user1.Decks.Add(deck);

            var user2 = new User("erwinb@basecone.nl", "Erwin Bonnet", userRepository);
            user2.Decks.Add(deck);

            Session.BeginTransaction();

            deckRepository.Save(deck);

            userRepository.CreateNewUser(user1, "test123");
            userRepository.CreateNewUser(user2, "test123");

            Session.Transaction.Commit();

            Session.Evict(deck);
            Session.Evict(user1);
            Session.Evict(user2);
            Session.Clear();

            var userFromDatabase1 = userRepository.Get(user1.Id);
            var userFromDatabase2 = userRepository.Get(user2.Id);

            Assert.AreEqual(1, userFromDatabase1.Decks.Count);
            Assert.AreEqual(1, userFromDatabase2.Decks.Count);
            Assert.AreEqual(deck, userFromDatabase1.Decks.First());
            Assert.AreEqual(deck, userFromDatabase2.Decks.First());
        }
    }
}
