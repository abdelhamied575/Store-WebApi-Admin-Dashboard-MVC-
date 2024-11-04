using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Services.Contract
{
    public interface ICashService
    {

        Task SetCashKeyAsync(string Key, object response, TimeSpan expireTime);

        Task<string> GetCashKeyAsync(string Key);



    }
}
