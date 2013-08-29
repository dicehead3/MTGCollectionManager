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
            deck.CardInDeck.Add(card);

            Assert.AreEqual(1, deck.CardInDeck.Count);
            Assert.AreEqual(card.Name, deck.CardInDeck[0].Name);
        }

        [Test]
        public void CanRemoveSingleCardFromDeck()
        {
            var deck = new Deck("Battle Blitz");
            var card = new Card("A", "Type", "Exp", CardRarity.Special, "Art", "Loc");
            var card2 = new Card("B", "Type", "Exp", CardRarity.Special, "Art", "Loc");

            deck.CardInDeck.Add(card);
            deck.CardInDeck.Add(card2);
            deck.CardInDeck.RemoveAt(1);

            Assert.AreNotEqual(2, deck.CardInDeck.Count);
            Assert.AreEqual(1, deck.CardInDeck.Count);
            Assert.AreEqual("A", deck.CardInDeck[0].Name);
        }
    }
}