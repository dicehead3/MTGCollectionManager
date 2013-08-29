﻿using Domain;
using FluentNHibernate.Mapping;

namespace Data.Mappings
{
    public class DeckMapping : ClassMap<Deck>
    {
        public DeckMapping()
        {
            Table("Decks");
            Id(x => x.Id);
            Map(x => x.Name)
                .Not.Nullable()
                .Length(9999)
                .Access.CamelCaseField(Prefix.Underscore);
            HasMany(x => x.CardInDeck)
                .AsBag()
                .Cascade.None()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
