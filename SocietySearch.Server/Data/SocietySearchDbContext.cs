using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Identity.Client;
using SocietySearch.Server.Model.Domain;
using System.Text.Json;

namespace SocietySearch.Server.Data
{
    public class SocietySearchDbContext:DbContext
    {
        public SocietySearchDbContext(DbContextOptions<SocietySearchDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<Society> Societies { get; set; }
        public DbSet<Units> Units { get; set; }

        //Data seeding using Entity Framework

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Units>()
                .ToTable("Units", tableBuilder => tableBuilder.HasCheckConstraint(
                    "CK_Units_Type_AllowedValues",
                    "[Type] IN ('1 BHK', '2 BHK', '3 BHK', '4 BHK', 'Penthouse', 'Studio')"));

            modelBuilder.Entity<Society>()
                .Property(s => s.AmenityIds)
                .HasConversion(
                    v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => v == null ? null : JsonSerializer.Deserialize<List<Guid?>>(v, (JsonSerializerOptions)null))
                .Metadata.SetValueComparer(new ValueComparer<List<Guid?>>(
                    (c1, c2) => (c1 ?? new List<Guid?>()).SequenceEqual(c2 ?? new List<Guid?>()),
                    c => (c ?? new List<Guid?>()).Aggregate(0, (a, v) => HashCode.Combine(a, v)),
                    c => c == null ? null : c.ToList()));

            modelBuilder.Entity<Amenities>().HasData(
                new Amenities
                {
                    Id = Guid.Parse("8de2be25-2200-4754-ac84-1e7955bcccb8"),
                    Name = "Swimming Pool",
                },
                new Amenities
                {
                    Id = Guid.Parse("0ccc5c1d-19c6-443e-a9ad-2c320acee3fa"),
                    Name = "Gym",
                },
                new Amenities
                {
                    Id = Guid.Parse("d0847646-fce2-4f68-80d3-cf28caac2d89"),
                    Name = "Clubhouse",
                },
                new Amenities
                {
                    Id = Guid.Parse("fdb8099b-54a2-4b55-8a00-575fc2c20130"),
                    Name = "Children's Play Area",
                },
                new Amenities
                {
                    Id = Guid.Parse("7d87193a-c059-428d-b1c8-2aa2df9874dc"),
                    Name = "CCTV",
                },
                new Amenities
                {
                    Id = Guid.Parse("989e00b5-0271-48b2-aa8a-14ace9d6b277"),
                    Name = "Parking",
                },
                new Amenities
                {
                    Id = Guid.Parse("6fc94294-f2e9-4653-8d89-9a48c050f45f"),
                    Name = "Library",
                },
                new Amenities
                {
                    Id = Guid.Parse("3928891d-c8f1-4348-ade7-8fe57534e8b4"),
                    Name = "Turf",
                });
        }
    }
}
