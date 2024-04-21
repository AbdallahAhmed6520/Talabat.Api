using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications(string sort, int? BrandId, int? TypeId)
            : base(p =>
            (!BrandId.HasValue || p.BrandId == BrandId)
            &&
            (!TypeId.HasValue || p.CategoryId == TypeId)
            )
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Brand);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
