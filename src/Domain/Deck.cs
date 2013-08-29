using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain
{
    public class Deck : Entity
    {
        private string _name;
        private readonly IList<Card> _cardInDeck = new List<Card>();

        public Deck(string name)
        {
            Name = name;
        }

        protected Deck()
        {
        }

        public virtual IList<Card> CardInDeck
        {
            get { return _cardInDeck; }
        }

        public virtual string Name
        {
            get { return _name; }
            protected set
            {
                _name = value.Required("Deckname can't be empty");
            }
        }
    }
}
