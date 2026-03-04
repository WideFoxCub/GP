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

        /// <summary>
        /// DbSet<Service> = tabela Services w bazie.
        /// Możemy queryować: _context.Services.ToListAsync()
        /// </summary>
        public DbSet<Service> Services { get; set; } = null!;

        /// <summary>
        /// Konfiguracja modeli (constraints, defaults, etc).
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja Service
            modelBuilder.Entity<Service>(entity =>
            {
                // Nazwa tabeli w DB
                entity.ToTable("Services");

                // Primary Key
                entity.HasKey(e => e.Id);

                // Kolumna Name - obowiązkowa, max 255 znaków
                entity.HasIndex(e => new { e.Name, e.Category })
                    .HasDatabaseName("idx_services_name_category")
                    .IsUnique();

                // Kolumna Description - obowiązkowa
                entity.Property(e => e.Description)
                    .IsRequired();

                // Kolumna PriceFrom - decimal(10, 2) z walidacją
                entity.Property(e => e.PriceFrom)
                    .HasColumnType("numeric(10,2)")
                    .IsRequired();

                // Kolumna CreatedAt - default do UTC Now
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd();

                // Kolumna IsActive - default true
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.Property(e => e.Category)
                    .HasConversion<string>()         // zapisuj jako "Nails" / "Cosmetology"
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasIndex(e => e.Category)
                    .HasDatabaseName("idx_services_category");

                // Indeksy dla lepszej wydajności
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
