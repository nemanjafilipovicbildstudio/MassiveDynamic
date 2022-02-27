using IdentityServer4.EntityFramework.Options;
using MassiveDynamic.Data.ModelConfigs;
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
                new IdentityRole { Id = "1", Name = RoleNames.Admin, NormalizedName = RoleNames.Admin.ToUpperInvariant(), ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = "2", Name = RoleNames.Secretary, NormalizedName = RoleNames.Secretary.ToUpperInvariant(), ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = "3", Name = RoleNames.Client, NormalizedName = RoleNames.Client.ToUpperInvariant(), ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = "4", Name = RoleNames.Guest, NormalizedName = RoleNames.Guest.ToUpperInvariant(), ConcurrencyStamp = Guid.NewGuid().ToString() }
            );

            // seed admin user, password "Admin_1"
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser 
                { 
                    Id = "11111111-1111-1111-1111-111111111111", 
                    FirstName = "Admin", 
                    LastName = "Admin", 
                    UserName = "admin", 
                    NormalizedUserName = "ADMIN",
                    Email = "admin@massivedynamic.com", 
                    NormalizedEmail = "ADMIN@MASSIVEDYNAMIC.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEMq8U0D1JQzZEaIU9ABn9HP6uLDvK/xs6LBgH+0wOtr4HC90np/tjSOM+jWcSyBssA==", 
                    EmailConfirmed = true 
                }
            );

            // admin user admin role
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "11111111-1111-1111-1111-111111111111", RoleId = "1" }
            );
        }
    }
}
