using GP.Contracts;

namespace GP.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync();

        Task<ServiceDto?> GetServiceByIdAsync(int id);

        Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto);

        Task<ServiceDto?> UpdateServiceAsync(int id, UpdateServiceDto updateServiceDto);

        Task<bool> DeleteServiceAsync(int id);
    }
}