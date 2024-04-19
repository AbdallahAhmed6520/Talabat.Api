using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        // /api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();

            var products = await _productsRepository.GetAllWithSpecAsync(spec);
            var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReturnToDTO>>(products);

            return Ok(MappedProducts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productsRepository.GetWithSpecAsync(spec);

            if (product is null)
                return NotFound(new ApiResponse(404)); // 404

            var MappedProduct = _mapper.Map<Product, ProductReturnToDTO>(product);

            return Ok(MappedProduct); // 200

        }
    }
}