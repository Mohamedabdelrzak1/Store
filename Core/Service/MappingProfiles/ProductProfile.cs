using AutoMapper;
using Domain.Models;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
    .ForMember(dest => dest.BrandName,
        opt => opt.MapFrom(src => src.productBrand.Name))
    .ForMember(dest => dest.TypeName,
        opt => opt.MapFrom(src => src.productType.Name))
    .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());
    //.ForMember(dest => dest.PictureUrl,
    //    opt => opt.MapFrom(src => $"https://localhost:44348/{src.PictureUrl}"));



            CreateMap<ProductType ,TypeDto>();
            CreateMap<ProductBrand ,BrandDto>();
        }

    }
}
