namespace SecondhandStore.ServiceExtension;

public static class AuthorizationService
{
    public static void AddAuthorizationService(this IServiceCollection services)
    {
        services.AddAuthorization(o =>
        {
            o.AddPolicy("AD", policy => policy.RequireClaim("AD"));
            o.AddPolicy("US", policy => policy.RequireClaim("US"));
        });
    }
}