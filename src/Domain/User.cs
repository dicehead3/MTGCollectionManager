using System;
using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain
{
    public class User : Entity
    {
        private string _name;
        private string _loginName;
        private string _password;
        private readonly IList<Card> _cards = new List<Card>();
        private readonly IList<Deck> _decks = new List<Deck>();

        public User(string name, string login, string password)
        {
            Name = name;
            LoginName = login;
            Password = password;
        }

        protected User()
        {
        }

        public virtual string Name
        {
            get
            {
                return _name;
            }
            protected set
            {
                _name = value.Required("User must have a name");
            }
        }

        public virtual string LoginName
        {
            get
            {
                return _loginName;
            }
            protected set
            {
                _loginName = value.Required("User must have login name");
            }
        }

        public virtual string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if(_password != null && _password.Equals(value))
                    throw new Exception("New password can't be equal to old password");
                _password = value.Required("User must have password");
            }
        }

        public virtual IList<Card> Cards
        {
            get { return _cards; }
        }

        public virtual IList<Deck> Decks
        {
            get { return _decks; }
        } 
    }
}
