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
                .Returns(!emailUnique);

            mock.Setup(x => x.EmailExists("noot@aap.mies"))
                .Returns(!emailUnique);

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

        [Test]
        public void CannotCreateUserWithExistingEmail()
        {
            var userRepository = CreateTestUserRepository(false);

            Assert.Throws<BusinessRuleViolationException>(
                () =>
                {
                    var user = new User("aap@noot.mies", "Jan", userRepository);
                });
        }

        [Test]
        public void CanAddRolesToUser()
        {
            var userRepository = CreateTestUserRepository(true);
            var user = new User("aap@noot.mies", "Jan", userRepository);

            user.Roles.Add(Role.Admin);
            user.Roles.Add(Role.User);

            Assert.AreEqual(2, user.Roles.Count);
            Assert.IsTrue(user.IsInRole(Role.Admin));
            Assert.IsTrue(user.IsInRole("user"));
        }

        [Test]
        public void CannotAddDuplicateRoles()
        {
            var userRepository = CreateTestUserRepository(true);
            var user = new User("aap@noot.mies", "Jan", userRepository);

            user.Roles.Add(Role.Admin);
            user.Roles.Add(Role.Admin);

            Assert.AreEqual(1, user.Roles.Count);
        }

        [Test]
        public void CanDetermineUserRole()
        {
            var userRepository = CreateTestUserRepository(true);
            var user = new User("aap@noot.mies", "Jan", userRepository);

            user.Roles.Add(Role.User);

            Assert.IsTrue(user.IsInRole(Role.User));
            Assert.IsFalse(user.IsInRole(Role.Admin));
        }

        [Test]
        public void CanGetIdentity()
        {
            var userRepository = CreateTestUserRepository(true);
            var user = new User("aap@noot.mies", "Jan", userRepository);

            Assert.AreEqual("aap@noot.mies", user.Identity.Name);
            Assert.IsTrue(user.Identity.IsAuthenticated);
        }
    }
}
