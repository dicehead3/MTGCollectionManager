using Domain;
using FluentNHibernate.Mapping;

namespace Data.Mappings
{
    class CardMapping : ClassMap<Card>
    {
        public CardMapping()
        {
            Table("Cards");
            Id(x => x.Id);
            Map(x => x.Name)
                .Not.Nullable()
                .Length(150)
                .Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Artist)
                .Not.Nullable()
                .Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.CardType)
                .Not.Nullable()
                .Length(9999)
                .Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Description)
                .Nullable()
                .Length(9999);
            Map(x => x.Expansion)
                .Not.Nullable()
                .Length(100)
                .Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.FlavorText)
                .Nullable()
                .Length(9999);
            Map(x => x.ImageLocation)
                .Not.Nullable()
                .Length(9999)
                .Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.ManaCost)
                .Nullable()
                .Length(20);
            Map(x => x.Power)
                .Nullable();
            Map(x => x.Rarity)
                .Not.Nullable()
                .Length(10);
            Map(x => x.Toughness)
                .Nullable();
            Map(x => x.ConvertedManaCost)
                .Nullable();
        }
    }
}
