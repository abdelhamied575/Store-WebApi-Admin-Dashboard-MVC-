using StoreWeb.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecifications<Order,int>
    {

        public OrderSpecifications(string buyerEmail,int orderId) 
            : base(O=>O.BuyerEmail==buyerEmail && O.Id==orderId)
        {
            ApplyIncudes();
        }



        public OrderSpecifications(string buyerEmail) 
            : base(O=>O.BuyerEmail==buyerEmail)
        {
            ApplyIncudes();
        }


        private void ApplyIncudes()
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }


    }
}
