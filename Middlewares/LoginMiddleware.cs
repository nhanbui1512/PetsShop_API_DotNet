
namespace petshop.Middlewares
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        public LoginMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Login Middleware");
            Console.WriteLine(context.Request.Path);

            await _next(context);
        }
    }
}