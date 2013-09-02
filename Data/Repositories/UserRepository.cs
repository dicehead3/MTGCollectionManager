using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.AbstractRepositories;
using NHibernate;
using NHibernate.Linq;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;

        public UserRepository(ISession session)
        {
            _session = session;
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

        public bool EmailExists(string email)
        {
            return _session.Query<User>().First(u => u.Email.Equals(email)) == null;
        }
    }
}
