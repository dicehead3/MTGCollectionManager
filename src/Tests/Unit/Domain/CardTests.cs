using Domain;
using NUnit.Framework;

namespace Tests.Unit.Domain
{
    [TestFixture]
    internal class CardTests
    {
        private const string Location = "C:\\Images\\Alara Reborn\\Deadcanyon Minotaur.png";
        private const string Name = "Deadcanyon Minotaur";
        private const string ManaCost = "3RG";
        private const string Type = "Creature - Minotaur";
        private const string Expansion = "Alara Reborn";
        private const CardRarity Rarity = CardRarity.Uncommon;
        private readonly int? _power = 5;
        private readonly int? _toughness = 4;
        private const string Artist = "John Doe";
        private const string FlavorText = "Comming at you.";
        private const string Desc = "Cycling {R/G}";

        [Test]
        public void CanCreateCard()
        {
            var card = new Card(Name, Type, Expansion, Rarity, Artist, Location)
            {
                ManaCost = ManaCost,
                Description = Desc,
                FlavorText = FlavorText,
                Power = _power,
                Toughness = _toughness
            };

            Assert.AreEqual(card.Artist, Artist);
            Assert.AreEqual(card.CardType, Type);
            Assert.AreEqual(card.Description, Desc);
            Assert.AreEqual(card.Expansion, Expansion);
            Assert.AreEqual(card.FlavorText, FlavorText);
            Assert.AreEqual(card.ImageLocation, Location);
            Assert.AreEqual(card.ManaCost, ManaCost);
            Assert.AreEqual(card.Name, Name);
            Assert.AreEqual(card.Power, _power);
            Assert.AreEqual(card.Rarity, Rarity);
            Assert.AreEqual(card.Toughness, _toughness);
        }
    }
}
