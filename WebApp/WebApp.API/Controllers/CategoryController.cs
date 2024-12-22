using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApp.Core.DTOs.Category;
using WebApp.Core.Entities;
using WebApp.DAL.Context;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        WebAppDbContext _db;

        public CategoryController(WebAppDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Category category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            return category is null
                ? StatusCode(StatusCodes.Status404NotFound)
                : StatusCode(StatusCodes.Status200OK, category);

        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return StatusCode(StatusCodes.Status200OK, await _db.Categories.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult> Create(CreateCategoryDto dto)
        {
            Category category = new Category()
            {
                Name = dto.Name,
            };
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, category);
        }


        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("ID cannot be null.");
            }

            Category category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
            {
                return NotFound("Category not found.");
            }

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut]
        public async Task<ActionResult> Update(UpdateCategoryDto dto)
        {
            Category category = _db.Categories.AsNoTracking().FirstOrDefault(c => c.Id == dto.Id);
            if (category is null) return NotFound("Category not found!");
            category.Name = dto.Name;
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();

            return Ok("Category updated.");
        }

    }
}
