using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pendding")]
        Pendding,

        [EnumMember(Value = "Payment Recived")]
        PaymentRecived,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed



    }
}
