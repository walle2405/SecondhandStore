using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SecondhandStore.Infrastructure;

namespace SecondhandStore.ServiceExtension;

public class CreateDbContextService
{
    public class AppDataContextFactory : IDesignTimeDbContextFactory<SecondhandStoreContext>
    {
        public SecondhandStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SecondhandStoreContext>();
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config["ConnectionStrings:SecondhandStoreDB"]);

            return new SecondhandStoreContext(optionsBuilder.Options);
        }
    }
}
