using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain
{
    public class Deck : Entity
    {
        private string _name;
        private readonly IDictionary<Card, int> _cardInDeck = new Dictionary<Card, int>();

        public Deck(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public IDictionary<Card, int> CardInDeck
        {
            get { return _cardInDeck; }
        }

        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value.Required("Deckname can't be empty");
            }
        }
    }
}
