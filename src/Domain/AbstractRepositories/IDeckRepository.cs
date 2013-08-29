using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.AbstractRepositories
{
    public interface IDeckRepository
    {
        Deck Get(int id);
        IList<Deck> Get(Expression<Func<Deck, bool>> expression);
        void Save(Deck deck);
    }
}
