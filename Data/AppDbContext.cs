using Microsoft.EntityFrameworkCore;
using CrisisManagementPlatform.Models;

namespace CrisisManagementPlatform.Data
{
    public class AppDbContext : DbContext
    {
        // Default constructor for EF Core design-time tools (Migrations)
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Crisis> Crises { get; set; }
        public DbSet<CrisisCategory> CrisisCategories { get; set; }
        public DbSet<AffectedArea> AffectedAreas { get; set; }
        public DbSet<CrisisAffectedArea> CrisisAffectedAreas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Fallback connection string if not provided via DI
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CrisisManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Composite Primary Key for Many-to-Many relationship (CrisisAffectedArea)
            modelBuilder.Entity<CrisisAffectedArea>()
                .HasKey(caa => new { caa.CrisisId, caa.AffectedAreaId });

            modelBuilder.Entity<CrisisAffectedArea>()
                .HasOne(caa => caa.Crisis)
                .WithMany(c => c.CrisisAffectedAreas)
                .HasForeignKey(caa => caa.CrisisId);

            modelBuilder.Entity<CrisisAffectedArea>()
                .HasOne(caa => caa.AffectedArea)
                .WithMany(aa => aa.CrisisAffectedAreas)
                .HasForeignKey(caa => caa.AffectedAreaId);
        }
    }
}