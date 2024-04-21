using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams Params)
            : base(p =>
            (!Params.BrandId.HasValue || p.BrandId == Params.BrandId)
            &&
            (!Params.TypeId.HasValue || p.CategoryId == Params.TypeId)
            )
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Brand);
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Brand);
        }
    }
}
