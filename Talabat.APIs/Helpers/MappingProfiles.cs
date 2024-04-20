using AutoMapper;
using Talabat.APIs.DTO;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductReturnToDTO>()
                .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
