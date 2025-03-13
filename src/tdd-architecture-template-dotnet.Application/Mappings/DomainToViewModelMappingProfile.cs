using AutoMapper;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Application.ViewModels.Sales;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Sales;
using tdd_architecture_template_dotnet.Domain.Entities.Users;

namespace tdd_architecture_template_dotnet.Application.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region Users Profile

            CreateMap<User, UserViewModel>();
            CreateMap<UserAddress, UserAddressViewModel>();
            CreateMap<UserContact, UserContactViewModel>();

            #endregion

            #region Products Profile

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductType, ProductTypeViewModel>();

            #endregion

            #region Sales Profile

            CreateMap<Sale, SaleViewModel>();

            #endregion
        }
    }
}
