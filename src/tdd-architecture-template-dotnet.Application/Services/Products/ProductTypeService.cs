﻿using AutoMapper;
using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.Services.Products.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Domain.Interfaces.Products;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger.Interfaces;

namespace tdd_architecture_template_dotnet.Application.Services.Products
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public ProductTypeService(IMapper mapper, IProductTypeRepository productTypeRepository, ILoggerService loggerService)
        {
            _productTypeRepository = productTypeRepository;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductTypeViewModel>>> GetAll()
        {
            try
            {
                var products = await _productTypeRepository.GetAll();

                if (products is null)
                {
                    _loggerService.LogInfo("Unable to identify product types in the database.");
                    return Result<IEnumerable<ProductTypeViewModel>>.Fail("Unable to identify product types in the database.", (int)HttpStatus.BadRequest);
                }

                var mapProducts = _mapper.Map<IEnumerable<ProductTypeViewModel>>(products);

                return Result<IEnumerable<ProductTypeViewModel>>.Ok(mapProducts);

            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error listing product types: " + ex.Message);
                return Result<IEnumerable<ProductTypeViewModel>>.Fail("There was an error listing product types: " + ex.Message);
            }

        }

        public async Task<Result<ProductTypeViewModel>> GetById(int id)
        {
            try
            {
                var product = await _productTypeRepository.GetById(id);

                if (product is null)
                {
                    _loggerService.LogInfo("Unable to identify product type in the database.");
                    return Result<ProductTypeViewModel>.Fail("Unable to identify product type in the database.", (int)HttpStatus.BadRequest);
                }

                var mapProduct = _mapper.Map<ProductTypeViewModel>(product);

                return Result<ProductTypeViewModel>.Ok(mapProduct);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error when searching for the product type: " + ex.Message);
                return Result<ProductTypeViewModel>.Fail("There was an error when searching for the product type: " + ex.Message);
            }

        }

        public async Task<Result<ProductTypeViewModel>> Put(ProductTypeViewModel productType)
        {
            try
            {
                var type = await _productTypeRepository.GetById(productType.Id);

                if (type is null)
                {
                    _loggerService.LogInfo("product type not found.");
                    return Result<ProductTypeViewModel>.Fail("product type not found.", (int)HttpStatus.BadRequest);
                }

                var mapProductType = _mapper.Map<ProductType>(productType);

                var result = await _productTypeRepository.Put(mapProductType);

                var mapProductTypeModel = _mapper.Map<ProductTypeViewModel>(result);

                return Result<ProductTypeViewModel>.Ok(mapProductTypeModel);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error editing the product type: " + ex.Message);
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
                _loggerService.LogError("There was an error registering the product type: " + ex.Message);
                return Result<ProductTypeViewModel>.Fail("There was an error registering the product type: " + ex.Message);
            }
        }
    }
}
