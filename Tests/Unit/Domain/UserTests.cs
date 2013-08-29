using System;
using Domain;
using Infrastructure.DomainBase;
using NUnit.Framework;

namespace Tests.Unit.Domain
{
    [TestFixture]
    class UserTests
    {
        private const string Name = "Piet";
        private const string LoginName = "Gast";
        private const string Pw = "1234";

        [Test]
        public void CanCreateUser()
        {
            var user = new User(Name, LoginName, Pw);
            Assert.AreEqual(Name, user.Name);
            Assert.AreEqual(LoginName, user.LoginName);
            Assert.AreEqual(Pw, user.Password);
        }

        [Test]
        public void CannotCreateUserWithoutName()
        {
            Assert.Throws<BusinessRuleViolationException>(() =>
            {
                new User(null, LoginName, Pw);
            });
        }

        [Test]
        public void CannotCreateUserWithoutLoginName()
        {
            Assert.Throws<BusinessRuleViolationException>(() => { new User(Name, null, Pw); });
        }

        [Test]
        public void CannotCreateUserWithoutPassword()
        {
            Assert.Throws<BusinessRuleViolationException>(() => { new User(Name, LoginName, ""); });
        }

        [Test]
        public void CannotMakeNewPasswordEqualToOldPassword()
        {
            Assert.Throws<Exception>(() =>
            {
                new User(Name, LoginName, Pw) {Password = "1234"};
            });
        }

        [Test]
        public void CanSetNewPasswordNotEqualToOldPassword()
        {
            new User(Name, LoginName, Pw) {Password = "2413"};
        }
    }
}
