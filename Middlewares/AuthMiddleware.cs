namespace petshop.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.User.Identity!.IsAuthenticated)
            {
                var userClaims = context.User.Claims;
                var userId = userClaims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                var userEmail = userClaims.FirstOrDefault(c => c.Type == "Email")?.Value;
            }

            await _next(context);
        }
    }
}