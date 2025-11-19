using Ecommerce.Service.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;


namespace Ecommerce.Presentation.Attributes
{
   
     
        public class RedisCacheAttribute : ActionFilterAttribute
        {
            public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

                var cacheKey = CreateCacheKey(context.HttpContext.Request);

                var cacheValue = await cacheService.GetAsync(cacheKey);
                if (cacheValue is not null)
                {
                    context.Result = new ContentResult
                    {
                        Content = cacheValue,
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                    return; 
                }

               
                var executedContext = await next.Invoke();

               
                if (executedContext.Result is ObjectResult objectResult)
                {
                    var serializedResult = System.Text.Json.JsonSerializer.Serialize(objectResult.Value);
                    await cacheService.SetAsync(cacheKey, serializedResult, TimeSpan.FromMinutes(5)); // TTL 5 دقائق كمثال
                }
            }

            private string CreateCacheKey(HttpRequest request)
            {
                StringBuilder key = new StringBuilder();
                key.Append(request.Path);

                foreach (var item in request.Query.OrderBy(x => x.Key))
                {
                    key.Append($"|{item.Key}-{item.Value}");
                }

                return key.ToString();
            }
        }
    }
