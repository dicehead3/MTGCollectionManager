using System;
using System.Collections.Generic;
using System.Security.Principal;
using Domain.AbstractRepositories;
using Infrastructure.DomainBase;

namespace Domain
{
    public class User : Entity, IPrincipal
    {
        private readonly IList<Card> _cards = new List<Card>();
        private readonly IList<Deck> _decks = new List<Deck>();
        private string _displayName;
        private string _email;
        private IIdentity _identity;
        private ISet<Role> _roles = new HashSet<Role>();

        public User(string email, string name, IUserRepository repository)
        {
            DisplayName = name;
            CheckEmail(email, repository);
        }

        protected User()
        {
        }

        private void CheckEmail(string email, IUserRepository repository)
        {
            var v = repository.EmailExists(email);
            if (!repository.EmailExists(email))
            {
                _email = email.Required("Please enter your email address").ValidEmailAddress();
            }
        }

        public virtual ISet<Role> Roles
        {
            get { return _roles; }
        } 

        public virtual string Email
        {
            get { return _email; }
        }

        public virtual string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value.Required("Please set a name to display."); }
        }

        public virtual IList<Card> Cards
        {
            get { return _cards; }
        }

        public virtual IList<Deck> Decks
        {
            get { return _decks; }
        }

        public IIdentity Identity
        {
            get { return _identity ?? (_identity = new GenericIdentity(Email)); }
        }

        public bool IsInRole(string role)
        {
            return IsInRole((Role) Enum.Parse(typeof (Role), role, true));
        }

        public bool IsInRole(Role role)
        {
            return _roles.Contains(role);
        }
    }
}
