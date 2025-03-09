using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Users;

namespace tdd_architecture_template_dotnet.Infrastructure.Data.EntitiesConfiguration.Users
{
    public class UserContactConfiguration : IEntityTypeConfiguration<UserContact>
    {
        public void Configure(EntityTypeBuilder<UserContact> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.CreationDate).IsRequired();

            builder.Property(p => p.ChangeDate);

            builder.Property(p => p.Email).IsRequired();

            builder.Property(p => p.PhoneNumber).IsRequired();

            builder.Property(p => p.UserId).IsRequired();

            builder
                .HasMany(ua => ua.User)
                .WithOne(u => u.Contact)
                .HasForeignKey(ua => ua.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new UserContact
                {
                    Id = 1,
                    CreationDate = DateTime.UtcNow,
                    ChangeDate = null,
                    Email = "usertest@test.com.br",
                    PhoneNumber = "+00 (00) 00000-0000",
                    UserId = 1,
                });
        }
    }
}
