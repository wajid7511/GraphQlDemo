using GraphQl.Abstractions;
using GraphQl.Database.Configurations;
using GraphQl.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl.Database
{
    public class GraphQlDatabaseContext : DbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public GraphQlDatabaseContext(
            DbContextOptions<GraphQlDatabaseContext> options,
            IDateTimeProvider dateTimeProvider
        )
            : base(options)
        {
            _dateTimeProvider =
                dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroceryConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost; Database=GraphQlDemo; User ID=SA;Password=P@ssw0rd.W;TrustServerCertificate=true;"
                );
            }
        }

        public override int SaveChanges()
        {
            SetAuditDates();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            SetAuditDates();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditDates()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(
                    e =>
                        e.Entity is DbBaseModel
                        && (e.State == EntityState.Added || e.State == EntityState.Modified)
                );

            foreach (var entry in entries)
            {
                var entity = (DbBaseModel)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = _dateTimeProvider.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {
                    entity.LastUpdateTime = _dateTimeProvider.UtcNow;
                }
            }
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Grocery> Groceries { get; set; }
    }
}
