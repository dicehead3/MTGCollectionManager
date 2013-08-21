using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NUnit.Framework;

namespace Tests.Domain
{
    [TestFixture]
    class DeckTests
    {
        [Test]
        public void CanCreateDeck()
        {
            var deck = new Deck(1, "Battle Blitz");
            Assert.AreEqual("Battle Blitz", deck.Name);
        }

        [Test]
        public void CanAddCardToDeck()
        {
            var deck = new Deck(1, "BattleBlitz");
            var card = new Card(25937, "C:\\", "Blaat", "Mèèèh");
            deck.CardInDeck.Add(new KeyValuePair<Card, int>(card, 1));

            Assert.AreEqual(card.Id, deck.CardInDeck.First().Key.Id);
            //Assert.AreEqual(card.Id, deck.CardInDeck.GetEnumerator().Current.Key.Id);
            //Assert.AreEqual(1, deck.CardInDeck.GetEnumerator().Current.Value);
            Assert.AreEqual(1, deck.CardInDeck[card]);
        }

        [Test]
        public void CanRemoveSingleCardFromDeck()
        {
            var deck = new Deck(1, "Battle Blitz");
            var card = new Card(25937, "C:\\", "Blaat", "Mèèèh");
            var card2 = new Card(58649, "D:\\", "Toil", "Trouble");

            deck.CardInDeck.Add(card,5);
            deck.CardInDeck.Add(card2, 2);

            deck.CardInDeck[card2] -= 1;
            Assert.AreNotEqual(2, deck.CardInDeck[card2]);
        }

        [Test]
        [ExpectedException(typeof (Exception))]
        public void CannotCreateDeckWithIdLessOrEqualToZero()
        {
            var deck = new Deck(0, "Blaat");
        }
    }
}