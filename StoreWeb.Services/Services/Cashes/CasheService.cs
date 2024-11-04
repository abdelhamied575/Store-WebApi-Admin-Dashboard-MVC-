using StackExchange.Redis;
using StoreWeb.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreWeb.Services.Services.Cashes
{
    public class CasheService : ICashService
    {
        private readonly IDatabase _database;

        public CasheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }


        public async Task<string> GetCashKeyAsync(string Key)
        {
           var casheResponse = await  _database.StringGetAsync(Key);

            if (casheResponse.IsNullOrEmpty) return null;
            return casheResponse.ToString();

        }

        public async Task SetCashKeyAsync(string Key, object response, TimeSpan expireTime)
        {
            if (response is null) return;

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await _database.StringSetAsync(Key,JsonSerializer.Serialize(response,options),expireTime);
        }
    }
}
