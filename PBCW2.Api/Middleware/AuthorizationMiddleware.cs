using PBCW2.Bussiness.Service.Auth;
using System.Text.Json;

namespace PBCW2.Api.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthService _authService;

        public AuthorizationMiddleware(RequestDelegate next, AuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            var authorizeAttribute = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>();
            if (authorizeAttribute != null)
            {
                var token = context.Request.Headers["Token"].ToString();

                if (!_authService.ValidateToken(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(ApiResponse<object>.ErrorResult("Unauthorized (Invalid Token)")));
                    return;
                }
            }

            await _next(context);
        }
    }
}
