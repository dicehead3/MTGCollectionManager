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
            HasManyToMany(x => x.Cards)
                .AsBag().Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
            HasManyToMany(x => x.Decks)
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
            HasMany(x => x.Roles)
                .Access.CamelCaseField(Prefix.Underscore)
                .AsSet()
                .Element("Role")
                .Cascade.AllDeleteOrphan();
        }
    }
}
