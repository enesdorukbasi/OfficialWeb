using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.ProgramEntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.CategoryId);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
            builder.Property(x => x.Keywords).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.IsShowMainPage).IsRequired();

            builder.HasMany(x => x.SubCategories).WithOne(x => x.ParentCategory).HasForeignKey(x => x.ParentCategoryId);
            builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
