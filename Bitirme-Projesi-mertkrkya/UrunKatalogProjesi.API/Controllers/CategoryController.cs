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
    public class CategoryController : BaseController<CategoryDto,Category>
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("{id:int}")]
        public new async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (!result.isSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost]
        public new async Task<IActionResult> CreateAsync([FromBody] CategoryDto category)
        {
            return await base.CreateAsync(category);
        }
        [HttpPut("{id:int}")]
        public new async Task<IActionResult> UpdateAsync(int id, [FromBody] CategoryDto category)
        {
            return await base.UpdateAsync(id,category);
        }
    }
}
