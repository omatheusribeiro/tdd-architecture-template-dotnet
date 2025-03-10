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
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Cache;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Cache.Interfaces;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger.Interfaces;

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

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserAddressRepository, UserAddresRepository>();
            services.AddTransient<IUserContactRepository, UserContactRepository>();

            #endregion

            #region Products Repositories

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();

            #endregion

            #region Sale Repository

            services.AddTransient<ISaleRepository, SaleRepository>();

            #endregion

            #region Singletons

            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ILoggerService, LoggerService>();

            #endregion

            return services;
        }
    }
}
