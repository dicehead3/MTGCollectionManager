using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AbstractRepositories
{
    public interface IUserRepository
    {
        User Get(int id);
        IList<User> Get(Expression<Func<User, bool>> expression);
        void Save(User user);
    }
}
