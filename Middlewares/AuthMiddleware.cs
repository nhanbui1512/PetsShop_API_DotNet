
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
            // Logic của bạn ở đây
            // Ví dụ: kiểm tra quyền truy cập
            // context.User.Identity.IsAuthenticated

            // Gọi middleware tiếp theo trong pipeline
            await _next(context);
        }
    }
}