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
                .ForMember(dist => dist.BrandName,
                 Options => Options.MapFrom(Src => Src.productBrand.Name))
                .ForMember(dist => dist.TypeName,
                 Options => Options.MapFrom(Src => Src.productType.Name));

            CreateMap<ProductType ,TypeDto>();
            CreateMap<ProductBrand ,BrandDto>();
        }

    }
}
