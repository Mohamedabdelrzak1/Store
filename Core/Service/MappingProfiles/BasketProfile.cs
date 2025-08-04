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
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            // Basket Items Mapping
            CreateMap<BasketItem, BasketItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ReverseMap()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

            // ✅ Basket Mapping
            CreateMap<CustomerBasket, BasketDto>()
                .ReverseMap();
        }
    }
}
