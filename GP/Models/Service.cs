namespace GP.Models
{
    /// <summary>
    /// Model Service - reprezentuje usługę w salonie.
    /// Odpowiada tabeli "Services" w bazie PostgreSQL.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Unikalny identyfikator usługi.
        /// EF Core automatycznie zrobi to Primary Key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nazwa usługi (np. "Manicure hybrydowy").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Opis usługi (co obejmuje, jak długo trwa, etc).
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Cena minimalna usługi.
        /// </summary>
        public decimal PriceFrom { get; set; }

        /// <summary>
        /// Data utworzenia rekordu (dla audytu).
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Czy usługa jest aktywna (widoczna dla klientów).
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
