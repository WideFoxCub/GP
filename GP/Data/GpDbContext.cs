using GP.Models;
using Microsoft.EntityFrameworkCore;

namespace GP.Data
{
    /// <summary>
    /// DbContext - reprezentuje sesję z bazą danych PostgreSQL.
    /// EF Core używa tej klasy do mapowania C# obiektów na tabele SQL.
    /// </summary>
    public class GpDbContext : DbContext
    {
        public GpDbContext(DbContextOptions<GpDbContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Services");
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.Name, e.Category })
                    .HasDatabaseName("idx_services_name_category")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired();

                entity.Property(e => e.PriceFrom)
                    .HasColumnType("numeric(10,2)")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.Property(e => e.Category)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasIndex(e => e.Category)
                    .HasDatabaseName("idx_services_category");

                entity.HasIndex(e => e.IsActive)
                    .HasName("idx_services_is_active");

                entity.HasIndex(e => e.CreatedAt)
                    .HasName("idx_services_created_at");

                entity.HasIndex(e => e.Name)
                    .HasName("idx_services_name")
                    .IsUnique();
            });
        }
    }
}
