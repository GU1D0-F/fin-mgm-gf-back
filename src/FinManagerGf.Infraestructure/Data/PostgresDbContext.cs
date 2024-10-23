using Microsoft.EntityFrameworkCore;

namespace FinManagerGf.Data
{
    public class PostgresDbContext(DbContextOptions<CoreDbContext> options) : CoreDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly); ?
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreDbContext).Assembly);
        }
    }
}
