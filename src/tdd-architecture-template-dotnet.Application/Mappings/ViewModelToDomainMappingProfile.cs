using AutoMapper;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Application.ViewModels.Sales;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Sales;
using tdd_architecture_template_dotnet.Domain.Entities.Users;

namespace tdd_architecture_template_dotnet.Application.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region Users Profile

            CreateMap<UserViewModel, User>();
            CreateMap<UserAddressViewModel, UserAddress>();
            CreateMap<UserContactViewModel, UserContact>();

            #endregion

            #region Products Profile

            CreateMap<ProductViewModel, Product>();
            CreateMap<ProductTypeViewModel, ProductType>();

            #endregion

            #region Sales Profile

            CreateMap<SaleViewModel, Sale>();

            #endregion
        }
    }
}
