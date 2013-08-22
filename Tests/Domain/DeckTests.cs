using System;
using System.Collections.Generic;
using System.Linq;
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
            var deck = new Deck("Battle Blitz");
            Assert.AreEqual("Battle Blitz", deck.Name);
        }

        [Test]
        public void CanAddCardToDeck()
        {
            var deck = new Deck("BattleBlitz");
            var card = new Card("a", null, "Artifact", "Alara Reborn", Card.CardRarity.Uncommon, null, null, null, null, "John Doe", "C");
            deck.CardInDeck.Add(new KeyValuePair<Card, int>(card, 1));

            Assert.AreEqual(1, deck.CardInDeck[card]);
        }

        [Test]
        public void CanRemoveSingleCardFromDeck()
        {
            var deck = new Deck("Battle Blitz");
            var card = new Card("a", null, "Artifact", "Alara Reborn", Card.CardRarity.Uncommon, null, null, null, null, "John Doe", "C");
            var card2 = new Card("b", null, "Artifact", "Alara Reborn", Card.CardRarity.Uncommon, null, null, null, null, "John Doe", "C");

            deck.CardInDeck.Add(card,5);
            deck.CardInDeck.Add(card2, 2);

            deck.CardInDeck[card2] -= 1;
            Assert.AreNotEqual(2, deck.CardInDeck[card2]);
        }
    }
}