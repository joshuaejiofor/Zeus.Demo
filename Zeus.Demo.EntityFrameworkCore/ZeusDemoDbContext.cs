using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Zeus.Demo.Core.Models;
using Zeus.Demo.EntityFrameworkCore.EntityConfigurations;

namespace Zeus.Demo.EntityFrameworkCore
{

    public class ZeusDemoDbContext(IConfiguration configuration) : DbContext(GetOptions(configuration))
    {
        private static DbContextOptions GetOptions(IConfiguration configuration)
        {
            // Used for unit tests only
            if (Convert.ToBoolean(configuration["IsUseInMemoryDB"]))
            {
                return new DbContextOptionsBuilder<ZeusDemoDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging().Options;
            }

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).AddInterceptors(new CommandInterceptor()).Options;
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).IsActive = true;
                    ((EntityBase)entity.Entity).CreatedOn = DateTime.Now;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Property("CreatedOn").IsModified = false;
                }
                else if (entity.State == EntityState.Deleted)
                {
                    ((EntityBase)entity.Entity).IsActive = false;
                }

                ((EntityBase)entity.Entity).UpdatedOn = DateTime.Now;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
            

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(default);
        }

    }
}
