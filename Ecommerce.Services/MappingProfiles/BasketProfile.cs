using AutoMapper;
using Ecommerce.Domain.Entities.BasketModule;
using Ecommerce.Shared.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile() 
        {
            CreateMap<CustomerBasketDTO, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDTO,BasketItem>().ReverseMap();
        }
    }
}
