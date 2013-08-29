using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.AbstractRepositories;
using NHibernate;
using NHibernate.Linq;

namespace Data.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly ISession _session;

        public DeckRepository(ISession session)
        {
            _session = session;
        }

        public Deck Get(int id)
        {
            return _session.Get<Deck>(id);
        }

        public IList<Deck> Get(Expression<Func<Deck, bool>> expression)
        {
            return _session.Query<Deck>().Where(expression).ToList();
        }

        public void Save(Deck deck)
        {
            _session.Save(deck);
        }
    }
}
