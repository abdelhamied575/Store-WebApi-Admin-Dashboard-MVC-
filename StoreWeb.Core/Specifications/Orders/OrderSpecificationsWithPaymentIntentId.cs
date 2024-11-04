using StoreWeb.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Specifications.Orders
{
    public class OrderSpecificationsWithPaymentIntentId :BaseSpecifications<Order,int>
    {

        public OrderSpecificationsWithPaymentIntentId(string paymentIntentId)
            :base(O=>O.PaymentIntentId==paymentIntentId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }


    }
}
