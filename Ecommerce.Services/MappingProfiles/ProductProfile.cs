using AutoMapper;
using Ecommerce.Domain.Entities.ProductModule;
using Ecommerce.Shared.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandsDTO>();
            CreateMap<ProductType, TypesDTO>();
            CreateMap<Products, ProductDTO>()
                .ForMember(dest => dest.ProductBrand, opt => opt
                .MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt
                .MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt
                .MapFrom<ProductPictureUrlResolver>());
                
        }
    }
}
