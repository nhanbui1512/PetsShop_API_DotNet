using petshop.Middlewares;

public static class MiddlewareExtensions
{
  public static IApplicationBuilder UseCustomMiddlewareForUser(this IApplicationBuilder app)
  {
    app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/users") && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase), appBuilder =>
    {
      appBuilder.UseMiddleware<AuthMiddleware>();
    });

    return app;
  }
}
