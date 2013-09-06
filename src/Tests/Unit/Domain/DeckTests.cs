using System.Collections.Generic;
using Domain;
using NUnit.Framework;

namespace Tests.Unit.Domain
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
            var card = new Card("Name", "Type", "Exp", CardRarity.Special, "Art", "Loc");
            deck.Cards.Add(card);

            Assert.AreEqual(1, deck.Cards.Count);
            Assert.AreEqual(card.Name, deck.Cards[0].Name);
        }

        [Test]
        public void CanRemoveSingleCardFromDeck()
        {
            var deck = new Deck("Battle Blitz");
            var card = new Card("A", "Type", "Exp", CardRarity.Special, "Art", "Loc");
            var card2 = new Card("B", "Type", "Exp", CardRarity.Special, "Art", "Loc");

            deck.Cards.Add(card);
            deck.Cards.Add(card2);
            deck.Cards.RemoveAt(1);

            Assert.AreNotEqual(2, deck.Cards.Count);
            Assert.AreEqual(1, deck.Cards.Count);
            Assert.AreEqual("A", deck.Cards[0].Name);
        }

        [Test]
        public void CanCreateManaCurve()
        {
            var deck = new Deck("deck");
            var card1 = new Card("A", "creature", "worldwake", CardRarity.Common, "walker", "dude")
            {
                ConvertedManaCost = 5
            };
            var card2 = new Card("B", "creature", "worldwake", CardRarity.Common, "walker", "dude")
            {
                ConvertedManaCost = 2
            };
            var card3 = new Card("C", "creature", "worldwake", CardRarity.Common, "walker", "dude")
            {
                ConvertedManaCost = 1
            };
            var card4 = new Card("D", "creature", "worldwake", CardRarity.Common, "walker", "dude")
            {
                ConvertedManaCost = 2
            };
            var card5 = new Card("E", "creature", "worldwake", CardRarity.Common, "walker", "dude")
            {
                ConvertedManaCost = 5
            };
            var card6 = new Card("F", "creature", "worldwake", CardRarity.Common, "walker", "dude")
            {
                ConvertedManaCost = 9
            };
            deck.Cards.Add(card1);
            deck.Cards.Add(card2);
            deck.Cards.Add(card3);
            deck.Cards.Add(card4);
            deck.Cards.Add(card5);
            deck.Cards.Add(card6);

            var curve = deck.ManaCurve;

            Assert.AreEqual(curve[2], 2);
            Assert.AreEqual(curve[9], 1);
            Assert.AreEqual(curve[5], 2);
            Assert.AreEqual(curve[1], 1);
            Assert.Throws<KeyNotFoundException>(() => { var c = curve[0]; });
        }
    }
}