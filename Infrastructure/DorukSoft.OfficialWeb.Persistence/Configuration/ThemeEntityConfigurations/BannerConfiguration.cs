using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.ThemeEntityConfigurations
{
    public class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.HasKey(x => x.BannerId);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
        }
    }
}
