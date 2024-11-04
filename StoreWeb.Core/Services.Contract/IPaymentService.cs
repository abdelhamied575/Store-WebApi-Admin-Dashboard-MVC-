using StoreWeb.Core.Dtos.Basket;
using StoreWeb.Core.Entities.Basket;
using StoreWeb.Core.Entities.Order;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Services.Contract
{
    public interface IPaymentService
    {


        Task<CustomerBasketDto> CreateOrUpdatePaymentIntentIdAsync(string basketId);

        Task<Order> UpdatePaymentIntentForSuccedOrFaild(string paymentIntentId, bool flag);



    }
}
