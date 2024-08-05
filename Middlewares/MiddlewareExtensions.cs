using petshop.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddlewareForUser(this IApplicationBuilder app)
    {

        var routesToProtect = new List<(string Path, string Method)>
        {
          ("/api/users", "GET"),
          ("/api/role", "POST"),
          ("/api/role", "GET"),
          ("/api/role", "PUT"),
          ("/api/orders/prepare", "PATCH"),
          ("/api/orders/confirm","PATCH")
        };
        foreach (var route in routesToProtect)
        {
            app.UseWhen(context => context.Request.Path.StartsWithSegments(route.Path) &&
                                   context.Request.Method.Equals(route.Method, StringComparison.OrdinalIgnoreCase),
            appBuilder =>
            {
                appBuilder.UseMiddleware<AdminMiddleware>();
            });
        }

        return app;
    }
}
