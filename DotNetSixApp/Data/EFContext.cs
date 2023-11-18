using DotNetSixApp.Models;
using Microsoft.EntityFrameworkCore;


namespace DotNetSixApp.Data
{
    public class EFContext : DbContext
    {
        private readonly IConfiguration _config;

        public EFContext(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DeafultConnection"), optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Users>()
                .ToTable("Users", "dbo") 
                .HasKey(u => u.UserId);
        }
    }




}
