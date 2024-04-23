using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.ProgramEntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Keywords).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Tax).IsRequired();
            builder.Property(x => x.Discount).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.ProductSalesType).IsRequired();

            builder.HasMany(x => x.ContactForProduct).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
        }
    }
}
