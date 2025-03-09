using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Products;

namespace tdd_architecture_template_dotnet.Infrastructure.Data.EntitiesConfiguration.Products
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.CreationDate).IsRequired();

            builder.Property(p => p.ChangeDate);

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.Description).IsRequired();

            builder
                .HasMany(u => u.Product)
                .WithOne(c => c.Type)
                .HasForeignKey(c => c.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new ProductType
                {
                    Id = 1,
                    CreationDate = DateTime.UtcNow,
                    ChangeDate = null,
                    Name = "Product type test",
                    Description = "Description for product type test",
                });
        }
    }
}
