﻿using Facebook.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Repository
{
    public class FacebookDatabaseContext : IdentityDbContext<User>
    {
        public FacebookDatabaseContext()
        {
        }

        public FacebookDatabaseContext(DbContextOptions options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=localhost;Database=Facebook;Trusted_Connection=true";
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");

            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityRole>().ToTable("Roles");

            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
    }
}