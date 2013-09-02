using System;
using System.Runtime.InteropServices;
using Domain;
using Domain.AbstractRepositories;
using Infrastructure.DomainBase;
using Moq;
using NUnit.Framework;

namespace Tests.Unit.Domain
{
    [TestFixture]
    class UserTests
    {
        private static IUserRepository CreateTestUserRepository(bool emailUnique)
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(x => x.EmailExists("aap@noot.mies"))
                .Returns(emailUnique);

            mock.Setup(x => x.EmailExists("noot@aap.mies"))
                .Returns(emailUnique);

            return mock.Object;
        }

        [Test]
        public void CanCreateUser()
        {
            var userRepository = CreateTestUserRepository(true);
            var user = new User("aap@noot.mies", "Jan", userRepository);

            Assert.AreEqual("aap@noot.mies", user.Email);
            Assert.AreEqual("Jan", user.DisplayName);
            Assert.AreEqual(0, user.Roles.Count);
            Assert.AreEqual(0, user.Decks.Count);
            Assert.AreEqual(0, user.Cards.Count);
        }
    }
}
