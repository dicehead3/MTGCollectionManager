using Domain;
using NUnit.Framework;

namespace Tests.Domain
{
    [TestFixture]
    class PersonalCollectionTests
    {
        [Test]
        public void CanCreateNewCollection()
        {
            var col = new PersonalCollection();
            Assert.AreEqual(col.Collection.Count, 0);
            Assert.AreEqual(col.Decks.Count, 0);
        }

        [Test]
        public void CanAddCardsToCollection()
        {
            var col = new PersonalCollection();
            var card = new Card("a", null, "Artifact", "Alara Reborn", Card.CardRarity.Uncommon, null, null, null, null, "John Doe", "C");
            col.Collection.Add(card);
            col.Collection.Add(card);

            Assert.AreEqual(card.Description, col.Collection[0].Description);
            Assert.AreEqual(card.FlavorText, col.Collection[0].FlavorText);
            Assert.AreEqual(card.ImageLocation, col.Collection[0].ImageLocation);
            Assert.AreEqual(card.Description, col.Collection[1].Description);
            Assert.AreEqual(card.FlavorText, col.Collection[1].FlavorText);
            Assert.AreEqual(card.ImageLocation, col.Collection[1].ImageLocation);
        }

        [Test]
        public void CanRemoveCardsFromCollection()
        {
            var col = new PersonalCollection();
            var c1 = new Card("a", null, "Artifact", "Alara Reborn", Card.CardRarity.Uncommon, null, null, null, null, "John Doe", "C");
            var c2 = new Card("b", null, "Artifact", "Alara Reborn", Card.CardRarity.Uncommon, null, null, null, null, "John Doe", "C");
            var c3 = new Card("c", null, "Artifact", "Alara Reborn", Card.CardRarity.Uncommon, null, null, null, null, "John Doe", "C");

            col.Collection.Add(c1);
            col.Collection.Add(c2);
            col.Collection.Add(c3);

            col.Collection.Remove(c2);
            
            Assert.AreNotEqual(c2.Name, col.Collection[1].Name);
            Assert.AreEqual(c3.Name, col.Collection[1].Name);
        }

        [Test]
        public void CanAddDeckToCollection()
        {
            var col = new PersonalCollection();
            var deck = new Deck("Blitz");
            col.Decks.Add(deck);
            
            Assert.AreEqual(deck.Name, col.Decks[0].Name);
        }

        [Test]
        public void CanRemoveDeckFromCollection()
        {
            var col = new PersonalCollection();
            var d1 = new Deck("A");
            var d2 = new Deck("B");
            var d3 = new Deck("C");

            col.Decks.Add(d1);
            col.Decks.Add(d2);
            col.Decks.Add(d3);

            col.Decks.Remove(d1);

            Assert.AreNotEqual(d1.Name, col.Decks[0].Name);
            Assert.AreEqual(d2.Name, col.Decks[0].Name);
            Assert.AreEqual(d3.Name, col.Decks[1].Name);
        }
    }
}
