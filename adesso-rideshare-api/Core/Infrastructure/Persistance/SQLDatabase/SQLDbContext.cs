using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistance.SQLDatabase
{
    public class SQLDBContext : DbContext
    {
        public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<TripBooking> TripBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                // TODO: Relations
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                // TODO: Relations
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trip");

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                // TODO: Relations
            });

            modelBuilder.Entity<TripBooking>(entity =>
            {
                entity.ToTable("TripBooking");

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                entity.HasOne(e => e.Trip)
                .WithMany(e => e.TripBookings)
                .HasForeignKey( e => e.TripId);
            });
        }
    }
}