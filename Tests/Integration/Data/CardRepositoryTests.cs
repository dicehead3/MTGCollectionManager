using Data.Repositories;
using Domain;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Integration.Data
{
    public class CardRepositoryTests : DataTestFixture
    {
        [Test]
        public void CanCreateCard()
        {
            var cardRepository = new CardRepository(Session);
            var card = new Card("Testcard", "testType", "testexpansion", CardRarity.Common, "erwin bonnet", "whereever");

            cardRepository.Save(card);

            Session.Evict(card);
            Session.Clear();

            var cardFromRepository = cardRepository.Get(card.Id);

            Assert.AreEqual("Testcard", cardFromRepository.Name);
        }
    }
}
