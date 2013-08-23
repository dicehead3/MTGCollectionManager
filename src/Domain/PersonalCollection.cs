using System.Collections.Generic;
using System.Linq;
using Infrastructure.DomainBase;

namespace Domain
{
    public class PersonalCollection : Entity
    {
        private readonly IList<Card> _collection = new List<Card>();
        private readonly IList<Deck> _createdDecks = new List<Deck>(); 

        public PersonalCollection()
        {
            
        }

        public IList<Card> Collection
        {
            get { return _collection; }
        }

        public IList<Deck> Decks
        {
            get
            {
                return _createdDecks;
            }
        }

        public IList<Card> GetUniqueList
        {
            get
            {
                return _collection.Distinct().ToList();
            }
        }
    }
}
