using System.Collections.Generic;
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
            var deck = new Deck(01, "Battle Blitz");
            Assert.AreEqual("Battle Blitz", deck.Name);
        }

        [Test]
        public void CanAddCardToDeck()
        {
            var deck = new Deck(01, "BattleBlitz");
            var card = new Card(15478, "Name", "Type", "Exp", CardRarity.Special, "Art", "Loc");
            deck.CardInDeck.Add(new KeyValuePair<Card, int>(card, 1));

            Assert.AreEqual(1, deck.CardInDeck[card]);
        }

        [Test]
        public void CanRemoveSingleCardFromDeck()
        {
            var deck = new Deck(453, "Battle Blitz");
            var card = new Card(15478, "A", "Type", "Exp", CardRarity.Special, "Art", "Loc");
            var card2 = new Card(15479, "B", "Type", "Exp", CardRarity.Special, "Art", "Loc");

            deck.CardInDeck.Add(card,5);
            deck.CardInDeck.Add(card2, 2);

            deck.CardInDeck[card2] -= 1;
            Assert.AreNotEqual(2, deck.CardInDeck[card2]);
        }
    }
}