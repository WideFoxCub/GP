using GP.Contracts;

namespace GP.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync(string? category);

        Task<ServiceDto?> GetServiceByIdAsync(int id);

        Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto);

        Task<ServiceDto?> UpdateServiceAsync(int id, UpdateServiceDto updateServiceDto);

        Task<bool> DeleteServiceAsync(int id);
    }
}