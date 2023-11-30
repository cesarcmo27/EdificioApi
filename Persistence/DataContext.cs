using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext :IdentityDbContext<AppUser,AppRol,string>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Building> Building { get; set; }
        public DbSet<Category> Categories { get; set; }


        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Person> Person { get; set; }

        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>()
            .HasMany(c => c.Categories)
            .WithOne(d => d.Group);

            builder.Entity<Building>()
            .HasMany(a => a.Apartments)
            .WithOne(b => b.Building)
            .HasForeignKey(c => c.BuildingId);

            builder.Entity<Person>()
         .HasOne(p => p.Apartment)
         .WithOne(a => a.Person)
         .HasForeignKey<Apartment>(a => a.PersonId); // Establece la clave foránea en la clase Apartment

            // Opcionalmente, puedes agregar una restricción única en la columna PersonId en la clase Apartment
            builder.Entity<Apartment>()
                .HasIndex(a => a.PersonId)
                .IsUnique();

            //builder.Entity<PeriodCategory>(a => a.HasKey(b => new{b.PeriodId,b.CategoryId}));

            //builder.Entity<Receipt>(x => x.HasKey(a => new { a.PeriodId, a.CategoryId,a.ApartmentId }));

            /* builder.Entity<Receipt>()
             .HasOne(u => u.PeriodCategory)
             .WithMany(a => a.Receipts)
             .HasForeignKey(b => new{b.PeriodId,b.CategoryId} );*/

            /* builder.Entity<Receipt>()
             .HasOne(u => u.Apartment)
             .WithMany(a => a.Receipts)
             .HasForeignKey(b => b.ApartmentId);*/
        }
    }
}