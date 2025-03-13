using AutoMapper;
using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.Services.Sales.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Sales;
using tdd_architecture_template_dotnet.Domain.Entities.Sales;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Domain.Interfaces.Products;
using tdd_architecture_template_dotnet.Domain.Interfaces.Sales;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;

namespace tdd_architecture_template_dotnet.Application.Services.Sales
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SaleService(
            ISaleRepository saleRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<SaleViewModel>>> GetAll()
        {
            try
            {
                var sales = await _saleRepository.GetAll();

                if (sales is null)
                    return Result<IEnumerable<SaleViewModel>>.Fail("Unable to identify sales in the database.", (int)HttpStatus.BadRequest);

                var mapSales = _mapper.Map<IEnumerable<SaleViewModel>>(sales);

                return Result<IEnumerable<SaleViewModel>>.Ok(mapSales);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SaleViewModel>>.Fail("There was an error listing sales: " + ex.Message);
            }

        }

        public async Task<Result<SaleViewModel>> GetById(int id)
        {
            try
            {
                var sale = await _saleRepository.GetById(id);

                if (sale is null)
                    return Result<SaleViewModel>.Fail("sale not found.", (int)HttpStatus.BadRequest);

                var mapSale = _mapper.Map<SaleViewModel>(sale);

                return Result<SaleViewModel>.Ok(mapSale);
            }
            catch (Exception ex)
            {
                return Result<SaleViewModel>.Fail("There was an error when searching for the sale: " + ex.Message);
            }
        }

        public async Task<Result<SaleViewModel>> Put(SaleViewModel sale)
        {
            try
            {
                var product = await _productRepository.GetById(sale.ProductId);

                var user = await _userRepository.GetById(sale.UserId);

                if (product is null)
                    return Result<SaleViewModel>.Fail("Product not found.", (int)HttpStatus.BadRequest);

                if (user is null)
                    return Result<SaleViewModel>.Fail("User not found.", (int)HttpStatus.BadRequest);

                var mapSale = _mapper.Map<Sale>(sale);

                var result = await _saleRepository.Put(mapSale);

                var mapSaleModel = _mapper.Map<SaleViewModel>(result);

                return Result<SaleViewModel>.Ok(mapSaleModel);
            }
            catch (Exception ex)
            {
                return Result<SaleViewModel>.Fail("There was an error editing the sale: " + ex.Message);
            }
        }

        public async Task<Result<SaleViewModel>> Post(SaleViewModel sale)
        {
            try
            {
                var product = await _productRepository.GetById(sale.ProductId);

                var user = await _userRepository.GetById(sale.UserId);

                if (product is null)
                    return Result<SaleViewModel>.Fail("Product not found.", (int)HttpStatus.BadRequest);

                if (user is null)
                    return Result<SaleViewModel>.Fail("User not found.", (int)HttpStatus.BadRequest);

                var mapSale = _mapper.Map<Sale>(sale);

                var result = await _saleRepository.Post(mapSale);

                var mapSaleModel = _mapper.Map<SaleViewModel>(result);

                return Result<SaleViewModel>.Ok(mapSaleModel);
            }
            catch (Exception ex)
            {
                return Result<SaleViewModel>.Fail("There was an error registering the sale: " + ex.Message);
            }
        }

        public async Task<Result<SaleViewModel>> Delete(SaleViewModel sale)
        {
            try
            {
                var product = await _productRepository.GetById(sale.ProductId);

                var user = await _userRepository.GetById(sale.UserId);

                if (product is null)
                    return Result<SaleViewModel>.Fail("Product not found.", (int)HttpStatus.BadRequest);

                if (user is null)
                    return Result<SaleViewModel>.Fail("User not found.", (int)HttpStatus.BadRequest);

                var mapSale = _mapper.Map<Sale>(sale);

                var result = await _saleRepository.Delete(mapSale);

                var mapSaleModel = _mapper.Map<SaleViewModel>(result);

                return Result<SaleViewModel>.Ok(mapSaleModel);
            }
            catch (Exception ex)
            {
                return Result<SaleViewModel>.Fail("There was an error deleting the sale: " + ex.Message);
            }

        }
    }
}
