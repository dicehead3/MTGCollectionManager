using System;

namespace Domain
{
    public class User
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
                if(string.IsNullOrWhiteSpace(value))
                    throw new Exception("User must have a name");
                _name = value;
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
                if(string.IsNullOrWhiteSpace(value))
                    throw new Exception("User must have login name");
                _loginName = value;
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
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("User must have password");
                if(_passWord != null && _passWord.Equals(value))
                    throw new Exception("New password can't be equal to old password");
                _passWord = value;
            }
        }
    }
}
