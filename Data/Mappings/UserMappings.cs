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
            Map(x => x.LoginName)
                .Not.Nullable()
                .Length(200)
                .Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Name)
                .Not.Nullable()
                .Length(200)
                .Access.CamelCaseField(Prefix.Underscore);
            Map(x => x.Password)
                .Not.Nullable()
                .Length(100)
                .Access.CamelCaseField(Prefix.Underscore);
            HasMany(x => x.Cards)
                .AsBag().Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
            HasMany(x => x.Decks)
                .AsBag().Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
