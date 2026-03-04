using GP.Models;

namespace GP.Data
{
    /// <summary>
    /// Klasa do inicjalizacji bazy danych z podstawowymi danymi.
    /// Seed data = domyślne dane wstawiane przy pierwszym starcie.
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(GpDbContext context)
        {
            // Jeśli są już usługi w bazie, nie dodawaj ponownie
            if (context.Services.Any())
            {
                return;
            }

            // Domyślne usługi salonu
            var services = new List<Service>
            {
                new()
                {
                    Name = "Manicure hybrydowy",
                    Description = "Profesjonalne opracowanie paznokci i aplikacja hybrydy. Trwa ok. 60 minut. Idealna do pracy biurowej.",
                    PriceFrom = 150m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Name = "Pedicure",
                    Description = "Kompleksowy zabieg pielęgnacyjny stóp z masażem i peellingiem. Trwa ok. 45 minut.",
                    PriceFrom = 180m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Name = "Zabieg na twarz",
                    Description = "Głębokie oczyszczanie, peepling i pielęgnacja twarzy. Idealny dla skóry problemowej. Trwa ok. 90 minut.",
                    PriceFrom = 200m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Name = "Upiększanie rzęs",
                    Description = "Laminacja i farbowanie rzęs, oraz tintowanie brwi. Efekt naturalny i piękny. Trwa ok. 45 minut.",
                    PriceFrom = 120m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Name = "Metamorfoza brwi",
                    Description = "Profesjonalne stylizowanie brwi, tintowanie i pielęgnacja. Indywidualne dopasowanie do kształtu twarzy.",
                    PriceFrom = 90m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Name = "Masaż relaksacyjny",
                    Description = "Całościowy masaż ciała o działaniu relaksującym. Doskonały do regeneracji i odprężenia. Trwa 60 minut.",
                    PriceFrom = 250m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            // Dodaj usługi do bazy
            context.Services.AddRange(services);
            context.SaveChanges();
        }
    }
}
