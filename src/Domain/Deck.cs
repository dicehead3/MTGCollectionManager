using System.Collections.Generic;
using System.Linq;
using Infrastructure.DomainBase;

namespace Domain
{
    public class Deck : Entity
    {
        private string _name;
        private readonly IList<Card> _cards = new List<Card>();

        public Deck(string name)
        {
            Name = name;
        }

        protected Deck()
        {
        }

        public virtual IList<Card> Cards
        {
            get { return _cards; }
        }

        public virtual string Name
        {
            get { return _name; }
            protected set
            {
                _name = value.Required("Deckname can't be empty");
            }
        }

        public virtual Dictionary<int, int> ManaCurve {
            get
            {
                return Cards.GroupBy(x => x.ConvertedManaCost)
                            .OrderBy(x => x.Key)
                            .ToDictionary(x => x.Key, x => x.Count());
            }
        }
    }
}
