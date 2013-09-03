﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.AbstractRepositories;
using Infrastructure.Encryption;
using NHibernate;
using NHibernate.Linq;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;
        private readonly IEncryptor _encryptor;

        public UserRepository(ISession session, IEncryptor encryptor)
        {
            _session = session;
            _encryptor = encryptor;
        }

        public User Get(int id)
        {
            return _session.Get<User>(id);
        }

        public IList<User> Get(Expression<Func<User, bool>> expression)
        {
            return _session.Query<User>().Where(expression).ToList();
        }

        public void Save(User user)
        {
            _session.Save(user);
        }

        public void CreateNewUser(User user, string password)
        {
            if (user.Id != 0)
            {
                throw new InvalidOperationException("User already exists");
            }
            _session.Save(user);

            const string query = "UPDATE Users SET Password = :Password WHERE Id = :UserId";

            _session.CreateSQLQuery(query)
                .SetInt32("UserId", user.Id)
                .SetString("Password", HashPassword(password))
                .UniqueResult();
        }

        public bool EmailExists(string email)
        {
            return _session.Query<User>().FirstOrDefault(u => u.Email.Equals(email)) != null;
        }

        public bool AuthenticateUser(string userEmail, string password)
        {
            const string query = "SELECT password FROM Users WHERE Email = :Email";

            var result = _session.CreateSQLQuery(query)
                .SetString("Email", userEmail)
                .UniqueResult<dynamic>();

            if (result == null)
                return false;

            var hashedPassword = result.ToString();

            return DoesPasswordMatch(password, hashedPassword);
        }

        private bool DoesPasswordMatch(string password, string passwordFromDatabase)
        {
            var pepper = passwordFromDatabase.Substring(0, 8);
            var salt = passwordFromDatabase.Substring(passwordFromDatabase.Length - 8, 8);

            var hash = pepper + _encryptor.MakeSha512Hash(salt + password + pepper + salt) + salt;
            return passwordFromDatabase.Equals(hash);
        }

        private string HashPassword(string password)
        {
            var random = new Random();
            var salt = String.Format("{0:x8}", random.Next(0x10000000));
            var pepper = String.Format("{0:x8}", random.Next(0x10000000));

            var hash = salt + password + pepper + salt;
            hash = _encryptor.MakeSha512Hash(hash);
            hash = pepper + hash + salt;
            return hash;
        }
    }
}
