using Microsoft.AspNetCore.Http;

namespace Api.Gateway.Proxies.Configuration
{
    public static class HttpClientTokenExtension
    {
        public static void AddBearerToken(this HttpClient httpClient, IHttpContextAccessor context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated && context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.HttpContext.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                }
            }
        }
    }
}
