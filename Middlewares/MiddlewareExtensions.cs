using petshop.Middlewares;

public static class MiddlewareExtensions
{
  public static IApplicationBuilder UseCustomMiddlewareForUser(this IApplicationBuilder app)
  {
    app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/users") && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase), appBuilder =>
    {
      appBuilder.UseMiddleware<AdminMiddleware>();
    });
    app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/role") && context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase), appBuilder =>
   {
     appBuilder.UseMiddleware<AdminMiddleware>();
   });

    return app;
  }
}
