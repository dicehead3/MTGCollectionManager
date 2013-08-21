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
    class CardTests
    {
        [Test]
        public void CanCreateCard()
        {
            const string location = "C:\\Images\\Alara Reborn\\Deadcanyon Minotaur.png";
            const string flavorText = "Comming at you.";
            const string desc = "Cycling {R/G}";
            const int id = 17536;

            var card = new Card(id, location, desc, flavorText);
            Assert.AreEqual(id, card.Id);
            Assert.AreEqual(location, card.CardImageLocation);
            Assert.AreEqual(desc, card.CardDescription);
            Assert.AreEqual(flavorText, card.CardFlavorText);
        }

        [Test]
        public void CanCreateCardWithoutFlavorText()
        {
            var card = new Card(14562, "C:\\", "Lifelink", null);
        }

        [Test]
        public void CanCreateCardWithoutDescriptionText()
        {
            var card = new Card(16356, "C:\\", null, "487. You're welcome!");
        }

        [Test]
        [ExpectedException(typeof (Exception))]
        public void CannotCreateCardWithoutId()
        {
            var card = new Card(0, "C:\\", null, null);
        }

        [Test]
        [ExpectedException(typeof (Exception))]
        public void CannotCreateCardWithoutImageLocation()
        {
            var card = new Card(89532, null, null, null);
        }
    }
}
