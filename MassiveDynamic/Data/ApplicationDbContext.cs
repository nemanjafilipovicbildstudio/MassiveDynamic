using IdentityServer4.EntityFramework.Options;
using MassiveDynamic.Data.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace MassiveDynamic.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasIndex(x => x.Email)
                .IsUnique();

            // seed roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = "2", Name = "Secretary", NormalizedName = "SECRETARY", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = "3", Name = "Client", NormalizedName = "CLIENT", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = "4", Name = "Guest", NormalizedName = "GUEST", ConcurrencyStamp = Guid.NewGuid().ToString() }
            );
        }
    }
}
