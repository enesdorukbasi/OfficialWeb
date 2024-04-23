using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.ProgramEntityConfigurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(x => x.BlogId);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
            builder.Property(x => x.IsShowMainPage).IsRequired();
            builder.Property(x => x.Keywords).IsRequired();
        }
    }
}
