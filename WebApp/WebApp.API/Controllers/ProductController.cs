using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.DTOs.Category;
using WebApp.Core.DTOs.Product;
using WebApp.Core.Entities;
using WebApp.DAL.Context;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        WebAppDbContext _db;

        public ProductController(WebAppDbContext db)
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

            Product product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

            return product is null
                ? StatusCode(StatusCodes.Status404NotFound)
                : StatusCode(StatusCodes.Status200OK, product);

        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return StatusCode(StatusCodes.Status200OK, await _db.Products.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult> Create(CreateProductDto dto)
        {
            Product product = new Product()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, product);
        }


        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("ID cannot be null.");
            }

            Product product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found.");
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut]
        public async Task<ActionResult> Update(UpdateProductDto dto)
        {
            Product product = _db.Products.AsNoTracking().FirstOrDefault(p => p.Id == dto.Id);
            if (product is null) return NotFound("Product not found!");
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();

            return Ok("Product updated.");
        }
    }
}
