using System;
using Infrastructure.DomainBase;

namespace Domain
{
    public class User : Entity
    {
        private string _name;
        private string _loginName;
        private string _passWord;

        public User(string name, string login, string password)
        {
            Name = name;
            LoginName = login;
            Password = password;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value.Required("User must have a name");
            }
        }

        public string LoginName
        {
            get
            {
                return _loginName;
            }
            private set
            {
                _loginName = value.Required("User must have login name");
            }
        }

        public string Password
        {
            get
            {
                return _passWord;
            }
            set
            {
                if(_passWord != null && _passWord.Equals(value))
                    throw new Exception("New password can't be equal to old password");
                _passWord = value.Required("User must have password");
            }
        }
    }
}
