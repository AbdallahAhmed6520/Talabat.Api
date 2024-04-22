using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductCategory> _catRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public ProductsController(IGenericRepository<Product> productsRepository,
            IMapper mapper,
            IGenericRepository<ProductCategory> catRepo,
            IGenericRepository<ProductBrand> brandRepo)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
            this._catRepo = catRepo;
            this._brandRepo = brandRepo;
        }

        // /api/Products
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductReturnToDTO>>> GetProducts([FromQuery] ProductSpecParams Params)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(Params);

            var products = await _productsRepository.GetAllWithSpecAsync(spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnToDTO>>(products);

            return Ok(new Pagination<ProductReturnToDTO>(Params.PageSize, Params.PageIndex, MappedProducts));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductReturnToDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductReturnToDTO>> GetProducts(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productsRepository.GetWithSpecAsync(spec);

            if (product is null)
                return NotFound(new ApiResponse(404)); // 404

            var MappedProduct = _mapper.Map<Product, ProductReturnToDTO>(product);

            return Ok(MappedProduct); // 200
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetTypes()
        {
            var categories = await _catRepo.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }
    }
}