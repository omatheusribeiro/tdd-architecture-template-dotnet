using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tdd_architecture_template_dotnet.Application.Services.Sales.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Sales;
using tdd_architecture_template_dotnet.Domain.Enums;

namespace tdd_architecture_template_dotnet.Controllers.V1.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("GetAllSales")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var response = await _saleService.GetAll();

            if (response == null || response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);

        }

        [HttpGet("GetSaleById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _saleService.GetById(id);

            if (response == null || response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);

        }

        [HttpPut("PutSale")]
        [Authorize]
        public async Task<ActionResult<SaleViewModel>> Put([FromBody] SaleViewModel sale)
        {
            var response = await _saleService.Put(sale);

            if (response == null || response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("PostSale")]
        [Authorize]
        public async Task<ActionResult<SaleViewModel>> Post([FromBody] SaleViewModel sale)
        {
            var response = await _saleService.Post(sale);

            if (response == null || response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("DeleteSale/{saleId:int}")]
        [Authorize]
        public async Task<ActionResult<SaleViewModel>> Delete([FromRoute] int saleId)
        {
            var response = await _saleService.Delete(saleId);

            if (response == null || response.StatusCode == (int)HttpStatus.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
