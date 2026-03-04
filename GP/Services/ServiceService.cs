using GP.Contracts;
using GP.Data;
using GP.Models;
using Microsoft.EntityFrameworkCore;

namespace GP.Services
{
    /// <summary>
    /// Implementacja serwisu dla usług salonu.
    /// Tutaj jest LOGIKA BIZNESOWA (pobieranie, filtrowanie, transformacja danych).
    /// Teraz: pobieranie z PostgreSQL przez EF Core.
    /// </summary>
    public class ServiceService : IServiceService
    {
        // DbContext - umożliwia komunikację z bazą
        private readonly GpDbContext _context;

        public ServiceService(GpDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            // Pobierz wszystkie AKTYWNE usługi z bazy danych
            var services = await _context.Services
                .Where(s => s.IsActive)
                .Select(s => new ServiceDto(
                    s.Id,
                    s.Name,
                    s.Description,
                    s.PriceFrom
                ))
                .ToListAsync();

            return services;
        }

        public async Task<ServiceDto?> GetServiceByIdAsync(int id)
        {
            // Pobierz usługę po ID z bazy
            var service = await _context.Services
                .Where(s => s.IsActive && s.Id == id)
                .Select(s => new ServiceDto(
                    s.Id,
                    s.Name,
                    s.Description,
                    s.PriceFrom
                ))
                .FirstOrDefaultAsync();

            return service;
        }

        public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            // Stwórz nowy model Service z DTO
            var service = new Service
            {
                Name = createServiceDto.Name,
                Description = createServiceDto.Description,
                PriceFrom = createServiceDto.PriceFrom,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            // Dodaj do bazy
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            // Zwróć DTO z ID (wygenerowanym przez bazę)
            return new ServiceDto(
                service.Id,
                service.Name,
                service.Description,
                service.PriceFrom
            );
        }

        public async Task<ServiceDto?> UpdateServiceAsync(int id, UpdateServiceDto updateServiceDto)
        {
            // Znajdź usługę w bazie
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return null;
            }

            // Zaktualizuj pola
            service.Name = updateServiceDto.Name;
            service.Description = updateServiceDto.Description;
            service.PriceFrom = updateServiceDto.PriceFrom;
            service.IsActive = updateServiceDto.IsActive;

            // Zapisz zmiany
            await _context.SaveChangesAsync();

            // Zwróć zaktualizowany DTO
            return new ServiceDto(
                service.Id,
                service.Name,
                service.Description,
                service.PriceFrom
            );
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            // Znajdź usługę w bazie
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return false;
            }

            // Soft delete - zmień IsActive na false
            service.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
