using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Users;

namespace tdd_architecture_template_dotnet.Infrastructure.Data.EntitiesConfiguration.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.CreationDate).IsRequired();

            builder.Property(p => p.ChangeDate);

            builder.Property(p => p.FirstName).IsRequired();

            builder.Property(p => p.LastName).IsRequired();

            builder.Property(p => p.Document).IsRequired();

            builder
               .HasOne(u => u.Contact)
               .WithMany(c => c.User)
               .HasForeignKey(c => c.Id)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(u => u.Address)
                .WithMany(a => a.User)
                .HasForeignKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(u => u.Sale)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new User
                {
                    Id = 1,
                    CreationDate = DateTime.UtcNow,
                    ChangeDate = null,
                    FirstName = "User",
                    LastName = "Test",
                    Document = "00.000.000/0000-00",
                });
        }
    }
}
