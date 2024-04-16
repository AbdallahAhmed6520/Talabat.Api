using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepository;

        public ProductsController(IGenericRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // /api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();

            var products = await _productsRepository.GetAllWithSpecAsync(spec);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var products = await _productsRepository.GetWithSpecAsync(spec);

            if (products is null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 }); // 404

            return Ok(products); // 200

        }
    }
}