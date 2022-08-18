using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrunKatalogProjesi.Core.Models;

namespace UrunKatalogProjesi.Data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private static string _connectionstring;

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AccountRefreshToken> AccountRefreshTokens { get; set; }
        public static void SetContextConnectionString(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql(_connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccountRefreshToken>(entity =>
            {
                entity.HasKey(r => r.UserId);
                entity.ToTable("accountRefreshToken");
            });
        }
    }
}