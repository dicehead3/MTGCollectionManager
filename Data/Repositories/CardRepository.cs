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
    public class CardRepository : ICardRepository
    {
        private readonly ISession _session;

        public CardRepository(ISession session)
        {
            _session = session;
        }

        public Card Get(int id)
        {
            return _session.Get<Card>(id);
        }

        public IList<Card> Get(Expression<Func<Card, bool>> expression)
        {
            return _session.Query<Card>().Where(expression).ToList();
        }

        public void Save(Card card)
        {
            _session.Save(card);
        }
    }
}
