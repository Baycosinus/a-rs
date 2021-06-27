using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistance.SQLDatabase
{
    public class SQLDbQueryContext : SQLDBContext
    {
        public SQLDbQueryContext(DbContextOptions<SQLDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("This context is read-only.");
        }
    }
}