using System;
using Domain;
using NUnit.Framework;

namespace Tests.Domain
{
    [TestFixture]
    internal class CardTests
    {
        private const string Location = "C:\\Images\\Alara Reborn\\Deadcanyon Minotaur.png";
        private const string Name = "Deadcanyon Minotaur";
        private const string ManaCost = "3RG";
        private const string Type = "Creature - Minotaur";
        private const string Expansion = "Alara Reborn";
        private const Card.CardRarity Rarity = Card.CardRarity.Uncommon;
        private int? Power = 5;
        private int? Toughness = 4;
        private const string Artist = "John Doe";
        private const string FlavorText = "Comming at you.";
        private const string Desc = "Cycling {R/G}";

        [Test]
        public void CanCreateCard()
        {
            var card = new Card(Name, ManaCost, Type, Expansion, Rarity, Desc, FlavorText, Power, Toughness, Artist,
                Location);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }

        [Test]
        public void CanCreateCardWithoutManaCost()
        {
            var card = new Card(Name, null, Type, Expansion, Rarity, Desc, FlavorText, Power, Toughness, Artist,
                Location);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, string.Empty);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }

        [Test]
        public void CanCreateCardWithoutDescription()
        {
            var card = new Card(Name, ManaCost, Type, Expansion, Rarity, null, FlavorText, Power, Toughness, Artist,
                Location);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, string.Empty);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }

        [Test]
        public void CanCreateCardWithoutFlavor()
        {
            var card = new Card(Name, ManaCost, Type, Expansion, Rarity, Desc, null, Power, Toughness, Artist,
                Location);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, string.Empty);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }

        [Test]
        public void CanCreateCardWithNullPower()
        {
            var card = new Card(Name, ManaCost, Type, Expansion, Rarity, Desc, FlavorText, null, Toughness, Artist,
                Location);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, null);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }

        [Test]
        public void CanCreateCardWithNullToughness()
        {
            var card = new Card(Name, ManaCost, Type, Expansion, Rarity, Desc, FlavorText, Power, null, Artist,
                Location);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, null);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotCreateCardWithoutName()
        {
            var card = new Card(null, ManaCost, Type, Expansion, Rarity, Desc, FlavorText, Power, Toughness, Artist,
                Location);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, null);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotCreateCardWithoutType()
        {
            var card = new Card(Name, ManaCost, null, Expansion, Rarity, Desc, FlavorText, Power, Toughness, Artist,
                Location);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, null);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotCreateCardWithoutExpansion()
        {
            var card = new Card(Name, ManaCost, Type, null, Rarity, Desc, FlavorText, Power, Toughness, Artist,
                Location);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, null);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotCreateCardWithoutArtist()
        {
            var card = new Card(null, ManaCost, Type, Expansion, Rarity, Desc, FlavorText, Power, Toughness, null,
                Location);

            Assert.AreEqual(card.Artist, null);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotCreateCardWithoutLocation()
        {
            var card = new Card(Name, ManaCost, Type, Expansion, Rarity, Desc, FlavorText, Power, Toughness, Artist,
                null);

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, null);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, Power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, Toughness);
        }
    }
}
