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

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync(string? category)
        {
            var query = _context.Services.Where(s => s.IsActive);

            if (!string.IsNullOrWhiteSpace(category))
            {
                var catNorm = category.Trim().ToLowerInvariant();
                if (catNorm == "nails")
                    query = query.Where(s => s.Category == ServiceCategory.Nails);
                else if (catNorm == "cosmetology")
                    query = query.Where(s => s.Category == ServiceCategory.Cosmetology);
                else
                    return Enumerable.Empty<ServiceDto>();
            }

            return await query
                .Select(s => new ServiceDto(
                    s.Id,
                    s.Name,
                    s.Description,
                    s.PriceFrom,
                    s.Category.ToString().ToLowerInvariant()
                ))
                .ToListAsync();
        }

        public async Task<ServiceDto?> GetServiceByIdAsync(int id)
        {
            return await _context.Services
                .Where(s => s.IsActive && s.Id == id)
                .Select(s => new ServiceDto(
                    s.Id,
                    s.Name,
                    s.Description,
                    s.PriceFrom,
                    s.Category.ToString().ToLowerInvariant()
                ))
                .FirstOrDefaultAsync();
        }

        public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            var cat = createServiceDto.Category.Trim().ToLowerInvariant();
            var categoryEnum = cat == "nails" ? ServiceCategory.Nails : ServiceCategory.Cosmetology;

            var service = new Service
            {
                Name = createServiceDto.Name,
                Description = createServiceDto.Description,
                PriceFrom = createServiceDto.PriceFrom,
                Category = categoryEnum,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return new ServiceDto(
                service.Id,
                service.Name,
                service.Description,
                service.PriceFrom,
                service.Category.ToString().ToLowerInvariant()
            );
        }

        public async Task<ServiceDto?> UpdateServiceAsync(int id, UpdateServiceDto updateServiceDto)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return null;

            var cat = updateServiceDto.Category.Trim().ToLowerInvariant();
            var categoryEnum = cat == "nails" ? ServiceCategory.Nails : ServiceCategory.Cosmetology;

            service.Name = updateServiceDto.Name;
            service.Description = updateServiceDto.Description;
            service.PriceFrom = updateServiceDto.PriceFrom;
            service.IsActive = updateServiceDto.IsActive;
            service.Category = categoryEnum;

            await _context.SaveChangesAsync();

            return new ServiceDto(
                service.Id,
                service.Name,
                service.Description,
                service.PriceFrom,
                service.Category.ToString().ToLowerInvariant()
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
