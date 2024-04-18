using Talabat.Core.Entities;

namespace Talabat.APIs.DTO
{
    public class ProductReturnToDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int BrandId { get; set; } // FK
        public string ProductBrand { get; set; }
        public int CategoryId { get; set; }
        public string ProductCategory { get; set; }
    }
}
