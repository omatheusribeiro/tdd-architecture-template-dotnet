using AutoMapper;
using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.Services.Products.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Domain.Interfaces.Products;

namespace tdd_architecture_template_dotnet.Application.Services.Products
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductTypeService(IMapper mapper, IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProductTypeViewModel>> Put(ProductTypeViewModel productType)
        {
            try
            {
                var type = await _productTypeRepository.GetById(productType.Id);

                if (type is null)
                    return Result<ProductTypeViewModel>.Fail("product type not found.", (int)HttpStatus.BadRequest);

                var mapProductType = _mapper.Map<ProductType>(productType);

                var result = await _productTypeRepository.Put(mapProductType);

                var mapProductTypeModel = _mapper.Map<ProductTypeViewModel>(result);

                return Result<ProductTypeViewModel>.Ok(mapProductTypeModel);
            }
            catch (Exception ex)
            {
                return Result<ProductTypeViewModel>.Fail("There was an error editing the product type: " + ex.Message);
            }
        }

        public async Task<Result<ProductTypeViewModel>> Post(ProductTypeViewModel productType)
        {
            try
            {

                var mapProductType = _mapper.Map<ProductType>(productType);

                var result = await _productTypeRepository.Post(mapProductType);

                var mapProductTypeModel = _mapper.Map<ProductTypeViewModel>(result);

                return Result<ProductTypeViewModel>.Ok(mapProductTypeModel);
            }
            catch (Exception ex)
            {
                return Result<ProductTypeViewModel>.Fail("There was an error registering the product type: " + ex.Message);
            }
        }
    }
}
