using Domain;
using FluentNHibernate.Mapping;

namespace Data.Mappings
{
    public class UserMappings : ClassMap<User>
    {
        public UserMappings()
        {
            Table("Users");
            Id(x => x.Id);
            HasMany(x => x.Cards)
                .AsBag().Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
            HasMany(x => x.Decks)
                .AsBag().Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.DisplayName)
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable()
                .Length(100);
            Map(x => x.Email)
                .Access.CamelCaseField(Prefix.Underscore)
                .Not.Nullable()
                .Length(200);
            Map(x => x.Identity)
                .Access.CamelCaseField(Prefix.Underscore);
            HasMany(x => x.Roles)
                .Access.CamelCaseField(Prefix.Underscore)
                .AsSet().Cascade.AllDeleteOrphan();
        }
    }
}
