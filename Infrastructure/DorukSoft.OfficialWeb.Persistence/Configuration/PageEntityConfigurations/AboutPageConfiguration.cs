using DorukSoft.OfficialWeb.Domain.PageEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.PageEntityConfigurations
{
    public class AboutPageConfiguration : IEntityTypeConfiguration<AboutPage>
    {
        public void Configure(EntityTypeBuilder<AboutPage> builder)
        {
            builder.HasKey(x => x.AboutId);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
        }
    }
}
