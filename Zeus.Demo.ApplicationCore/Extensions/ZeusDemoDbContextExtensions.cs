using Zeus.Demo.EntityFrameworkCore;

namespace Zeus.Demo.ApplicationCore.Extensions
{
    public static class ZeusDemoDbContextExtensions
    {
        public static async Task SeedDB(this ZeusDemoDbContext context)
        {
            bool changes = false;

            if (changes) await context.SaveChangesAsync();
        }
    }
}
