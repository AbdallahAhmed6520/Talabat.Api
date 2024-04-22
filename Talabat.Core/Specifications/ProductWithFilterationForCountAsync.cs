using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFilterationForCountAsync : BaseSpecifications<Product>
    {
        public ProductWithFilterationForCountAsync(ProductSpecParams Params):
            base(p =>
            (!Params.BrandId.HasValue || p.BrandId == Params.BrandId)
            &&
            (!Params.TypeId.HasValue || p.CategoryId == Params.TypeId))
        {
            
        }
    }
}
