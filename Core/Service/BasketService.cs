using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using ServiceAbstraction;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<BasketDto?> GetBasketAsync(string Id)
        {
            var basket = await basketRepository.GetBasketAsync(Id);
            if (basket is null) throw new BasketNotFoundException(Id);
            var result = mapper.Map<BasketDto>(basket);
            return result;
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);
            basket = await basketRepository.UpdateBasketAsync(basket);
            if (basket is null) throw new BasketCreateOrUpdateBadRequestException();
            var result = mapper.Map<BasketDto>(basket);
            return result;
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
            var flag = await basketRepository.DeleteBasketAsync(Id);
            if (flag == false) throw new BasketDeletebadRequest();
            return flag;
        }
    }
}
