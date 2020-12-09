using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasIndex(p => new { p.Name, p.Address })
                .IsUnique();

            modelBuilder.Entity<TelephoneNumber>()
               .HasIndex(p => new { p.ContactId, p.Number })
               .IsUnique();

            modelBuilder.Entity<TelephoneNumber>().HasData(Seed.Numbers);
            modelBuilder.Entity<Contact>().HasData(Seed.Contacts);

            // global filters
            modelBuilder.Entity<Contact>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<TelephoneNumber>().HasQueryFilter(p => !p.Deleted);
        }

        public DbSet<Contact> Contacts { get; set; }

    }
}
