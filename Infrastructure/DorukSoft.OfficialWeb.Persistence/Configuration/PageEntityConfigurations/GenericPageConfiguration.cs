using DorukSoft.OfficialWeb.Domain.PageEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.PageEntityConfigurations
{
    public class GenericPageConfiguration : IEntityTypeConfiguration<GenericPage>
    {
        public void Configure(EntityTypeBuilder<GenericPage> builder)
        {
            builder.HasKey(x => x.PageId);
            builder.Property(x => x.PageTitle).IsRequired();
            builder.Property(x => x.PageContent).IsRequired();
            builder.Property(x => x.PageType).IsRequired();
        }
    }
}
