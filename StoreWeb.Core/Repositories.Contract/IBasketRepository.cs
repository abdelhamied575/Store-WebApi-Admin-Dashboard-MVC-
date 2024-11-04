using StoreWeb.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Repositories.Contract
{
    public interface IBasketRepository
    {
            Task<CustomerBasket?> GetBasketAsync(string basketId);
            Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket); // Add & Update 

            Task<bool> DeleteBasketAsync(string basketId);

    }
}
