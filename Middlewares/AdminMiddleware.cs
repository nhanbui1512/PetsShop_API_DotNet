namespace petshop.Middlewares
{
    public class AdminMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminMiddleware(RequestDelegate next)
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

                if (userId != "1")
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden; // or any other status code
                    await context.Response.WriteAsync("Forbidden: You do not have access.");
                    return; // End the request here
                }
            }

            await _next(context);
        }
    }
}