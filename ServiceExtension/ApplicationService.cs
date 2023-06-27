using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;

namespace SecondhandStore.ServiceExtension;

public static class ApplicationService
{
    public static void AddApplicationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SecondhandStoreContext>(
            option => option.UseSqlServer(
                configuration["ConnectionStrings:SecondhandStoreDB"],
                b
                    => b.MigrationsAssembly(typeof(SecondhandStoreContext).Assembly.FullName)));
    }
}