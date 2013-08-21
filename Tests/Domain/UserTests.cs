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
    class UserTests
    {
        private const string name = "Piet";
        private const string loginName = "Gast";
        private const string pw = "1234";
        private const int id = 0;

        [Test]
        public void CanCreateUser()
        {
            var user = new User(id, name, loginName, pw);
            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(loginName, user.LoginName);
            Assert.AreEqual(pw, user.Password);
            Assert.AreEqual(id, user.Id);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotCreateUserWithoutName()
        {
            var user = new User(id, null, loginName, pw);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotCreateUserWithNegativeId()
        {
            var user = new User(-7, name, loginName, pw);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotCreateUserWithoutLoginName()
        {
            var user = new User(id, name, null, pw);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotCreateUserWithoutPassword()
        {
            var user = new User(id, name, loginName, "");
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void CannotMakeNewPasswordEqualToOldPassword()
        {
            var user = new User(id, name, loginName, pw);
            user.Password = "1234";
        }

        [Test]
        public void CanSetNewPasswordNotEqualToOldPassword()
        {
            var user = new User(id, name, loginName, pw);
            user.Password = "2413";
        }
    }
}
