using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocietySearch.Server.Model.Domain;

namespace SocietySearch.Server.Data
{
    public class SocietySearchAuthDbContext:IdentityDbContext
    {
        public SocietySearchAuthDbContext(DbContextOptions<SocietySearchAuthDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Society lives in the separate main database; it must not be mapped as an entity here.
            builder.Entity<Manager>().Ignore(m => m.Societies);

            var managerRoleId = "a2ee0a60-5a52-4946-b166-6a2f4c7dc6fd";
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Id = managerRoleId,
                    Name = "Manager",
                    NormalizedName = "Manager".ToUpper(),
                    ConcurrencyStamp = managerRoleId

                }
            );
                
        }
    }
}
