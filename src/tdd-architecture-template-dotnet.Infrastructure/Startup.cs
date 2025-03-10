using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tdd_architecture_template_dotnet.Domain.Interfaces.Products;
using tdd_architecture_template_dotnet.Domain.Interfaces.Sales;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Products;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Sales;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Users;
using tdd_architecture_template_dotnet.Infrastructure.Singleton.Cache;
using tdd_architecture_template_dotnet.Infrastructure.Singleton.Logger;

namespace tdd_architecture_template_dotnet.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                      .EnableSensitiveDataLogging());

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            #region Users Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAddressRepository, UserAddresRepository>();
            services.AddScoped<IUserContactRepository, UserContactRepository>();

            #endregion

            #region Products Repositories

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();

            #endregion

            #region Sale Repository

            services.AddScoped<ISaleRepository, SaleRepository>();

            #endregion

            #region Singletons

            services.AddSingleton<CacheService>();
            services.AddSingleton<LoggerService>();

            #endregion

            return services;
        }
    }
}
