using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Deck
    {
        private int _id;
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

        public int Id
        {
            get { return _id; }
            private set
            {
                if(value <= 0)
                    throw new Exception("Deck id cant be 0 or less");
                _id = value;
            }
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
