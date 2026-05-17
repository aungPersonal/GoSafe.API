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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Force EF Core to use the exact table name "tblUser" 
            // instead of the pluralized "tblUsers"
            modelBuilder.Entity<tblUser>().ToTable("tblUser");
            modelBuilder.Entity<tblRouteTemplate>().ToTable("tblRouteTemplate");
        }
    }
}
