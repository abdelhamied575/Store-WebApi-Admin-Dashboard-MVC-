using AutoMapper;
using StoreWeb.Core.Dtos.Basket;
using StoreWeb.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Mapping.Basket
{
    public class BasketProfile :Profile
    {

        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem >().ReverseMap();
        }


    }
}
