using Shared.Data.Seed;

namespace Catalog.Data.Seed;
public class CatalogDataSeed(CatalogDbContext context)
    : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        if (!await context.Products.AnyAsync())
        {
            await context.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }
}
