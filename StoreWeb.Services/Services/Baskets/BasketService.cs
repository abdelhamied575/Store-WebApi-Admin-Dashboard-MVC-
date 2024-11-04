using AutoMapper;
using StackExchange.Redis;
using StoreWeb.Core.Dtos.Basket;
using StoreWeb.Core.Entities.Basket;
using StoreWeb.Core.Repositories.Contract;
using StoreWeb.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Services.Services.Baskets
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        public async Task<bool> DeleteBasketAsync(string basketId)
        {
           return await _basketRepository.DeleteBasketAsync(basketId);
        }

        public async Task<CustomerBasketDto?> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null) return _mapper.Map<CustomerBasketDto>(new CustomerBasket() { Id = basketId });

            return _mapper.Map<CustomerBasketDto>(basket);
 
        }

        public async Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketDto)
        {
            var basket = await _basketRepository.UpdateBasketAsync(_mapper.Map<CustomerBasket>(basketDto));

            if (basket is null) return null;

            return _mapper.Map<CustomerBasketDto>(basket);

             
        }

        
    }
}
