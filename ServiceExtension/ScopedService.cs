using SecondhandStore.Repository;
using SecondhandStore.Services;

namespace SecondhandStore.ServiceExtension;

public static class ScopedService
{
    public static void AddScopedService(this IServiceCollection services)
    {
        services.AddScoped<RoleRepository>();
        services.AddScoped<RoleService>();

        services.AddScoped<AccountRepository>();
        services.AddScoped<AccountService>();

        services.AddScoped<PostRepository>();
        services.AddScoped<PostService>();

        services.AddScoped<TopUpRepository>();
        services.AddScoped<TopUpService>();

        services.AddScoped<ReportRepository>();
        services.AddScoped<ReportService>();

        services.AddScoped<ExchangeOrderRepository>();
        services.AddScoped<ExchangeOrderService>();

        services.AddScoped<AzureStorageRepository>();
        services.AddScoped<AzureService>();

        services.AddScoped<ReviewRepository>();
        services.AddScoped<ReviewService>();

        // services.AddScoped<IEmailService>();
        // services.AddScoped<EmailService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ImageRepository>();
        services.AddScoped<ImageService>();

        services.AddScoped<ReportImageRepository>();
        services.AddScoped<ReportImageService>();
    }
}