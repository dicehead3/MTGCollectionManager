using FluentNHibernate.Mapping;
using Infrastructure.Translations;

namespace Data.Mappings
{
    public class TranslationMap : ClassMap<Translation>
    {
        public TranslationMap()
        {
            Table("Translations");

            Id(x => x.Id);

            Map(x => x.Culture);
            Map(x => x.Code);
            Map(x => x.Text).Length(9999);
        }
    }
}
