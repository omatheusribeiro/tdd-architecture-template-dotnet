using AutoMapper;
using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.Services.Products.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Domain.Interfaces.Products;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger.Interfaces;

namespace tdd_architecture_template_dotnet.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            IProductTypeRepository productTypeRepository,
            ILoggerService loggerService,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductViewModel>>> GetAll()
        {
            try
            {
                var products = await _productRepository.GetAll();

                if (products is null)
                {
                    _loggerService.LogInfo("Unable to identify products in the database.");
                    return Result<IEnumerable<ProductViewModel>>.Fail("Unable to identify products in the database.", (int)HttpStatus.BadRequest);
                }

                var mapProducts = _mapper.Map<IEnumerable<ProductViewModel>>(products);

                return Result<IEnumerable<ProductViewModel>>.Ok(mapProducts);

            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error listing products: " + ex.Message);
                return Result<IEnumerable<ProductViewModel>>.Fail("There was an error listing products: " + ex.Message);
            }

        }

        public async Task<Result<ProductViewModel>> GetById(int id)
        {
            try
            {
                var product = await _productRepository.GetById(id);

                if (product is null)
                {
                    _loggerService.LogInfo("Unable to identify product in the database.");
                    return Result<ProductViewModel>.Fail("Unable to identify product in the database.", (int)HttpStatus.BadRequest);
                }

                var mapProduct = _mapper.Map<ProductViewModel>(product);

                return Result<ProductViewModel>.Ok(mapProduct);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error when searching for the product: " + ex.Message);
                return Result<ProductViewModel>.Fail("There was an error when searching for the product: " + ex.Message);
            }

        }

        public async Task<Result<ProductViewModel>> Put(ProductViewModel product)
        {
            try
            {
                var productType = await _productTypeRepository.GetById(product.ProductTypeId);

                if (productType is null)
                {
                    _loggerService.LogInfo("product type not found.");
                    return Result<ProductViewModel>.Fail("product type not found.", (int)HttpStatus.BadRequest);
                }

                var mapProduct = _mapper.Map<Product>(product);

                var result = await _productRepository.Put(mapProduct);

                var mapProductModel = _mapper.Map<ProductViewModel>(result);

                return Result<ProductViewModel>.Ok(mapProductModel);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error editing the product: " + ex.Message);
                return Result<ProductViewModel>.Fail("There was an error editing the product: " + ex.Message);
            }
        }

        public async Task<Result<ProductViewModel>> Post(ProductViewModel product)
        {
            try
            {
                var productType = await _productTypeRepository.GetById(product.ProductTypeId);

                if (productType is null)
                {
                    _loggerService.LogInfo("product type not found.");
                    return Result<ProductViewModel>.Fail("product type not found.", (int)HttpStatus.BadRequest);
                }

                var mapProduct = _mapper.Map<Product>(product);

                var result = await _productRepository.Post(mapProduct);

                var mapProductModel = _mapper.Map<ProductViewModel>(result);

                return Result<ProductViewModel>.Ok(mapProductModel);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error registering the product: " + ex.Message);
                return Result<ProductViewModel>.Fail("There was an error registering the product: " + ex.Message);
            }
        }

        public async Task<Result<ProductViewModel>> Delete(ProductViewModel product)
        {
            try
            {
                var productType = await _productTypeRepository.GetById(product.ProductTypeId);

                if (productType is null)
                {
                    _loggerService.LogInfo("product type not found.");
                    return Result<ProductViewModel>.Fail("product type not found.", (int)HttpStatus.BadRequest);
                }

                var mapProduct = _mapper.Map<Product>(product);

                var result = await _productRepository.Delete(mapProduct);

                var mapProductModel = _mapper.Map<ProductViewModel>(result);

                return Result<ProductViewModel>.Ok(mapProductModel);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error deleting the product: " + ex.Message);
                return Result<ProductViewModel>.Fail("There was an error deleting the product: " + ex.Message);
            }

        }
    }
}
