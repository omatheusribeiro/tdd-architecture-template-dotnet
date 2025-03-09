using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Users;

namespace tdd_architecture_template_dotnet.Infrastructure.Data.EntitiesConfiguration.Users
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.CreationDate).IsRequired();

            builder.Property(p => p.ChangeDate);

            builder.Property(p => p.Street).IsRequired();

            builder.Property(p => p.Number).IsRequired();

            builder.Property(p => p.Complement);

            builder.Property(p => p.City).IsRequired();

            builder.Property(p => p.State).IsRequired();

            builder.Property(p => p.Country).IsRequired();

            builder.Property(p => p.ZipCode).IsRequired();

            builder.Property(p => p.UserId).IsRequired();

            builder
               .HasMany(ua => ua.User)
               .WithOne(u => u.Address)
               .HasForeignKey(ua => ua.Id)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new UserAddress
                {
                    Id = 1,
                    CreationDate = DateTime.UtcNow,
                    ChangeDate = null,
                    Street = "Street test",
                    Number = 1,
                    Complement = null,
                    City = "City test",
                    State = "State test",
                    Country = "Country test",
                    ZipCode = "00000-000",
                    UserId = 1,
                });
        }
    }
}
