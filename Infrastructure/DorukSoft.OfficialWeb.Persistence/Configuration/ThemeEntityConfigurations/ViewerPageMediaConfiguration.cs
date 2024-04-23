using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.ThemeEntityConfigurations
{
    public class ViewerPageMediaConfiguration : IEntityTypeConfiguration<ViewerPageMedia>
    {
        public void Configure(EntityTypeBuilder<ViewerPageMedia> builder)
        {
            builder.HasKey(x => x.MediaId);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.MediaExtension).IsRequired();
            builder.Property(x => x.MediaUrl).IsRequired();
            builder.Property(x => x.ViewerPageId).IsRequired();
        }
    }
}
