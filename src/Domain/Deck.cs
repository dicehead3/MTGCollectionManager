using System;
using System.Collections.Generic;

namespace Domain
{
    public class Deck
    {
        private string _name;
        private readonly IDictionary<Card, int> _cardInDeck = new Dictionary<Card, int>();

        public Deck(string name)
        {
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
                if(string.IsNullOrWhiteSpace(value))
                    throw new Exception("Deckname can't be empty");
                _name = value;
            }
        }
    }
}
