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
            var card = new Card(15478, "Name", "Type", "Exp", CardRarity.Special, "Art", "Loc");
            col.Collection.Add(card);
            col.Collection.Add(card);

            Assert.AreEqual(card.Id, col.Collection[0].Id);
            Assert.AreEqual(card.Id, col.Collection[1].Id);
        }

        [Test]
        public void CanRemoveCardsFromCollection()
        {
            var col = new PersonalCollection();
            var c1 = new Card(15478, "A", "Type", "Exp", CardRarity.Special, "Art", "Loc");
            var c2 = new Card(15479, "B", "Type", "Exp", CardRarity.Special, "Art", "Loc");
            var c3 = new Card(15480, "C", "Type", "Exp", CardRarity.Special, "Art", "Loc");

            col.Collection.Add(c1);
            col.Collection.Add(c2);
            col.Collection.Add(c3);

            col.Collection.Remove(c2);
            
            Assert.AreNotEqual(c2.Id, col.Collection[1].Id);
            Assert.AreEqual(c3.Id, col.Collection[1].Id);
        }

        [Test]
        public void CanAddDeckToCollection()
        {
            var col = new PersonalCollection();
            var deck = new Deck(01, "Blitz");
            col.Decks.Add(deck);
            
            Assert.AreEqual(deck.Name, col.Decks[0].Name);
        }

        [Test]
        public void CanRemoveDeckFromCollection()
        {
            var col = new PersonalCollection();
            var d1 = new Deck(01, "A");
            var d2 = new Deck(02, "B");
            var d3 = new Deck(03, "C");

            col.Decks.Add(d1);
            col.Decks.Add(d2);
            col.Decks.Add(d3);

            col.Decks.Remove(d1);

            Assert.AreNotEqual(d1.Id, col.Decks[0].Id);
            Assert.AreEqual(d2.Id, col.Decks[0].Id);
            Assert.AreEqual(d3.Id, col.Decks[1].Id);
        }
    }
}
