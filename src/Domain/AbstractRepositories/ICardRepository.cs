using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.AbstractRepositories
{
    public interface ICardRepository
    {
        Card Get(int id);
        IList<Card> Get(Expression<Func<Card, bool>> expression); 
        void Save(Card card);
    }
}
