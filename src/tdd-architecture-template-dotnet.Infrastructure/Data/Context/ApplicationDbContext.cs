using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Sales;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Infrastructure.Data.EntitiesConfiguration.Products;
using tdd_architecture_template_dotnet.Infrastructure.Data.EntitiesConfiguration.Sales;
using tdd_architecture_template_dotnet.Infrastructure.Data.EntitiesConfiguration.Users;

namespace tdd_architecture_template_dotnet.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region Users Tables

        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        #region Products Tables

        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }

        #endregion

        #region Sales Table

        public DbSet<Sale> Sales { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Users Configuration

            builder.ApplyConfiguration(new UserAddressConfiguration());

            builder.ApplyConfiguration(new UserContactConfiguration());

            builder.ApplyConfiguration(new UserConfiguration());

            #endregion

            #region Products Configuration

            builder.ApplyConfiguration(new ProductTypeConfiguration());

            builder.ApplyConfiguration(new ProductConfiguration());

            #endregion

            #region Sales Configuration

            builder.ApplyConfiguration(new SaleConfiguration());

            #endregion
        }
    }
}
