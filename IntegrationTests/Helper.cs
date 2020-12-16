using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTests
{
    public static class Helper
    {
        public static WebApplicationFactory<TEntryPoint> WithInMemoryDatabase<TEntryPoint, TDbContext>(this WebApplicationFactory<TEntryPoint> factory, string dbName = "InMemoryDbForTesting") where TEntryPoint : class where TDbContext : DbContext
        {
            var newFactory = factory.WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureServices(services =>
                    {

                        services.RemoveAll<DbContextOptions<TDbContext>>();

                        services.AddDbContext<TDbContext>(options =>
                        {
                            options.UseInMemoryDatabase(dbName);
                        });
                    });
                });
            return newFactory;
        }
    }

}