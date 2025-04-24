using Microsoft.Extensions.DependencyInjection;
using tdd_architecture_template_dotnet.Application.Services.Login.Interfaces;
using tdd_architecture_template_dotnet.Application.Services.Login;
using tdd_architecture_template_dotnet.Application.Services.Products.Interfaces;
using tdd_architecture_template_dotnet.Application.Services.Products;
using tdd_architecture_template_dotnet.Application.Services.Sales.Interfaces;
using tdd_architecture_template_dotnet.Application.Services.Sales;
using tdd_architecture_template_dotnet.Application.Services.Users.Interfaces;
using tdd_architecture_template_dotnet.Application.Services.Users;
using tdd_architecture_template_dotnet.Infrastructure.Authentication;

namespace tdd_architecture_template_dotnet.Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Authentication Services
            services.AddScoped<TokenGenerator>();
            #endregion

            #region Users Services

            services.AddScoped<IUserService, UserService>();

            #endregion

            #region Products Services

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();

            #endregion

            #region Sales Services

            services.AddScoped<ISaleService, SaleService>();

            #endregion

            #region Login Services

            services.AddScoped<ILoginService, LoginService>();

            #endregion

            return services;
        }
    }
}
