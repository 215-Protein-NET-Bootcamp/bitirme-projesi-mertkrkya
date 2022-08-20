using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrunKatalogProjesi.Controllers;
using UrunKatalogProjesi.Data.Dto;
using UrunKatalogProjesi.Data.Models;
using UrunKatalogProjesi.Service.Services.Abstract;

namespace UrunKatalogProjesi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : BaseController<ProductDto, Product>
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) : base(productService)
        {
            _productService = productService;
        }
        [HttpGet("{id:int}")]
        public new async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            if (!result.isSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost]
        public new async Task<IActionResult> CreateAsync([FromBody] ProductDto product)
        {
            return await base.CreateAsync(product);
        }
        [HttpPut("{id:int}")]
        public new async Task<IActionResult> UpdateAsync(int id, [FromBody] ProductDto product)
        {
            return await base.UpdateAsync(id, product);
        }
    }
}
