using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IgenericRepository<Product> _productRepo;

        public ProductController(IgenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
    }
}
