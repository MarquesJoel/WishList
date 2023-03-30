using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WishList.API.DTOs;
using WishList.API.Models;
using WishList.API.Repository;

namespace WishList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly WishListDbContext _wishListDbContext;

        public ProductsController(
            ILogger<ProductsController> logger,
            WishListDbContext wishListDbContext)
        {
            _logger = logger;
            _wishListDbContext = wishListDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Description = productDto.Description,
                URL = productDto.URL
            };

            await _wishListDbContext.Products.AddAsync(product);
            await _wishListDbContext.SaveChangesAsync();

            var response = new ProductResposeDto
            {
                Description = product.Description,
                Id = product.Id,
                URL = product.URL
            };

            return CreatedAtRoute(nameof(GetProductAsync), new { product.Id }, response);
        }

        [HttpGet("{id}", Name = nameof(GetProductAsync))]
        public async Task<IActionResult> GetProductAsync(long id)
        {
            var product = await _wishListDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var response = new ProductResposeDto
            {
                Description = product.Description,
                Id = product.Id,
                URL = product.URL
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _wishListDbContext.Products.ToListAsync();

            var response = products.Select(
                p => new ProductResposeDto
                {
                    Description = p.Description,
                    Id = p.Id,
                    URL = p.URL
                });

            return Ok(response);
        }

    }
}