using AutoMapper;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.BasketModule;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDTO> CreateOrUpdateBasketAsync(CustomerBasketDTO createOrUpdate)
        {
           var basket = _mapper.Map<CustomerBasket>(createOrUpdate);

            var updated = await _basketRepository.CreateOrUpdateBasketAsync(basket);

            if (updated is null) return null!;

            return _mapper.Map<CustomerBasketDTO>(updated);
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }

        public async Task<CustomerBasketDTO> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null) return null!;

            return _mapper.Map<CustomerBasketDTO>(basket);
        }
    }
}
