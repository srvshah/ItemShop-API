using ItemShop.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItemShop.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
       
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ItemShopDbContext>();

            dbContext.Database.Migrate();
        }
       
    }
}
