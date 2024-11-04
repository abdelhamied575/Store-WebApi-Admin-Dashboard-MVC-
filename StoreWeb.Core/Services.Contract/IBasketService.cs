using StoreWeb.Core.Dtos.Basket;
using StoreWeb.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Services.Contract
{
    public interface IBasketService
    {

        Task<CustomerBasketDto?> GetBasketAsync(string basketId);
        Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketDto); // Add & Update 

        Task<bool> DeleteBasketAsync(string basketId);



    }
}
