using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.AbstractRepositories
{
    public interface IUserRepository
    {
        User Get(int id);
        IList<User> Get(Expression<Func<User, bool>> expression);
        void CreateNewUser(User user, string password);
        void Save(User user);
        bool EmailExists(string email);
        bool AuthenticateUser(string userEmail, string password);
        ChangePassswordMessage ChangePassword(string userEmail, string oldPassword, string newPassword, string confirmNewPassword);
        void ResetPassword(string userEmail);
    }
}
