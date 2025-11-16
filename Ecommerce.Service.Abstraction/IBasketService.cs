using Ecommerce.Shared.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Abstraction
{
    public interface IBasketService
    {
        Task<CustomerBasketDTO> CreateOrUpdateBasketAsync(CustomerBasketDTO createOrUpdate);
        Task<CustomerBasketDTO> GetBasketAsync(string basketId);
        Task<bool> DeleteBasketAsync(string basketId);
        
    }
}
