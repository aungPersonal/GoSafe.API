using Microsoft.EntityFrameworkCore;

namespace GoSafe.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<tblRouteTemplate> tblRouteTemplates { get; set; }
        public DbSet<configRole> configRoles { get; set; }
        public DbSet<tblBus> tblBuses { get; set; }
        public DbSet<tblToken> tblTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblUser>().ToTable("tblUser");
            modelBuilder.Entity<tblRouteTemplate>().ToTable("tblRouteTemplate");
            modelBuilder.Entity<configRole>().ToTable("configRole");
            modelBuilder.Entity<tblBus>().ToTable("tblBus");
            modelBuilder.Entity<tblToken>().ToTable("tblToken");
           
        }
    }
}
