using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.ThemeEntityConfigurations
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.HasKey(x => x.SliderId);
            builder.Property(x => x.SliderImageUrl).IsRequired();
            builder.Property(x => x.SliderContent).IsRequired();
        }
    }
}
