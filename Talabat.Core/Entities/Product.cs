using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        //[ForeignKey(nameof(Product.Brand))]
        public int ProductBrandId { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductBrand Brand { get; set; }

        public ProductCategory Category { get; set; }
    }
}
