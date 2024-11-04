using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWeb.Core.Services.Contract;
using System.Text;

namespace StoreWebApi.Attributes
{
    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTime;

        public CashedAttribute(int expireTime)
        {
            _expireTime = expireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var casheService= context.HttpContext.RequestServices.GetRequiredService<ICashService>();

            var casheKey=GenerateCasheKeyFromRequest(context.HttpContext.Request);

            var casheResponse = await casheService.GetCashKeyAsync(casheKey);

            if(!string.IsNullOrEmpty(casheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = casheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result=contentResult;
                return;
            }

            var executedContext = await next();

            if(executedContext.Result is OkObjectResult response)
            {
               await casheService.SetCashKeyAsync(casheKey,response.Value,TimeSpan.FromHours(_expireTime));
            }
            

        }

        private string GenerateCasheKeyFromRequest(HttpRequest request)
        {
            var casheKey = new StringBuilder();
            casheKey.Append($"{request.Path}");
            foreach ( var (Key,value) in request.Query.OrderBy(x => x.Key))
            {
                casheKey.Append($"|{Key}-{value}");
            }

            return casheKey.ToString();
        }

    }
}
