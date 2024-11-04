using AutoMapper;
using Microsoft.Extensions.Configuration;
using StoreWeb.Core.Dtos.Auth;
using StoreWeb.Core.Dtos.Orders;
using StoreWeb.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Mapping.Orders
{
    public class OrderProfile : Profile
    {


        public OrderProfile(IConfiguration configuration)
        {
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, options => options.MapFrom(d => d.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, options => options.MapFrom(d => d.DeliveryMethod.Cost))
                ;

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, options => options.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, options => options.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(s => $"{configuration["BaseURL"]}{s.Product.PictureUrl}"))
                ;


        }





    }
}
