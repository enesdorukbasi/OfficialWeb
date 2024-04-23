using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.ProgramEntityConfigurations
{
    public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
    {
        public void Configure(EntityTypeBuilder<SocialMedia> builder)
        {
            builder.HasKey(x => x.SocialMediaId);
            builder.Property(x => x.Url).IsRequired();
            builder.Property(x => x.Icon).IsRequired();
        }
    }
}
