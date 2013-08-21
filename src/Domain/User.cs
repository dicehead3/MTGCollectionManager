using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        private int _id;
        private string _name;
        private string _loginName;
        private string _passWord;

        public User(int id, string name, string login, string password)
        {
            Id = id;
            Name = name;
            LoginName = login;
            Password = password;
        }

        public int Id
        {
            get
            {
                return _id;
            }
            private set
            {
                if(value < 0)
                    throw new Exception("Id must be greater then zero");
                _id = value;
            }
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
