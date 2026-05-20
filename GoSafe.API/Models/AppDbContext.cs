using Microsoft.EntityFrameworkCore;

namespace GoSafe.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TblUser> TblUsers { get; set; }
        public DbSet<TblRouteTemplate> tblRouteTemplates { get; set; }
        public DbSet<ConfigRole> ConfigRoles { get; set; }
        public DbSet<TblBus> TblBuses { get; set; }
        public DbSet<TblToken> TblTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUser>().ToTable("TblUser");
            modelBuilder.Entity<TblRouteTemplate>().ToTable("TblRouteTemplate");
            modelBuilder.Entity<ConfigRole>().ToTable("ConfigRole");

            modelBuilder.Entity<TblBus>().ToTable("TblBus");
            //.Property(x => x.CreatedAt).HasColumnType("timestamp without  time zone");
            modelBuilder.Entity<TblBus>().ToTable("TblBus");
              //.Property(x => x.UpdatedAt).HasColumnType("timestamp without  time zone");

            modelBuilder.Entity<TblToken>().ToTable("TblToken");
           
        }
    }
}
