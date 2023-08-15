
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VNWalks.API.Data
{
    public class VNWalksAuthDbContext : IdentityDbContext
    {
        public VNWalksAuthDbContext(DbContextOptions<VNWalksAuthDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Seeding Roles
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "28110613-090e-4d1f-bc57-440fc1547069";
            var writerRoleId = "cf9467b7-537c-4234-8c97-574f67609504";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                   Id =  readerRoleId,
                   ConcurrencyStamp = readerRoleId,
                   Name = "Reader",
                   NormalizedName = "Reader".ToUpper(),

                },

                new IdentityRole
                {
                   Id =  writerRoleId,
                   ConcurrencyStamp = writerRoleId,
                   Name = "Writer",
                   NormalizedName = "Writer".ToUpper(),

                },

            };

            builder.Entity<IdentityRole>().HasData(roles);

        }


    }
}
