namespace GP.Models
{
    /// <summary>
    /// Model Service - reprezentuje usługę w salonie.
    /// Odpowiada tabeli "Services" w bazie PostgreSQL.
    /// </summary>
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal PriceFrom { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public ServiceCategory Category { get; set; }
    }
}