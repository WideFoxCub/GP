using GP.Contracts;
using GP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace GP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        // Dependency Injection - .NET automatycznie injektuje serwis
        private readonly IServiceService _serviceService;
        private readonly IValidator<CreateServiceDto> _createValidator;
        private readonly IValidator<UpdateServiceDto> _updateValidator;

        public ServicesController(
            IServiceService serviceService,
            IValidator<CreateServiceDto> createValidator,
            IValidator<UpdateServiceDto> updateValidator)
        {
            _serviceService = serviceService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        /// <summary>
        /// Pobiera wszystkie usługi dostępne w salonie.
        /// GET: api/services
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> Get([FromQuery] string? category)
        {
            var services = await _serviceService.GetAllServicesAsync(category);
            return Ok(services);
        }

        /// <summary>
        /// Pobiera usługę po ID.
        /// GET: api/services/1
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetById(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);

            if (service == null)
            {
                return NotFound($"Usługa o ID {id} nie znaleziona");
            }

            return Ok(service);
        }

        /// <summary>
        /// Tworzy nową usługę w salonie.
        /// POST: api/services
        /// Body: { "name": "...", "description": "...", "priceFrom": 100 }
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ServiceDto>> Create([FromBody] CreateServiceDto createServiceDto)
        {
            // Walidacja danych
            var validationResult = await _createValidator.ValidateAsync(createServiceDto);

            if (!validationResult.IsValid)
            {
                // Zwróć błędy walidacji
                var errors = validationResult.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new
                {
                    message = "Dane wejściowe są niepoprawne",
                    errors = errors
                });
            }

            // Stwórz usługę w bazie
            var service = await _serviceService.CreateServiceAsync(createServiceDto);

            // Zwróć 201 Created + lokalizacja nowego zasobu
            return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
        }

        /// <summary>
        /// Aktualizuje istniejącą usługę.
        /// PUT: api/services/1
        /// Body: { "name": "...", "description": "...", "priceFrom": 100, "isActive": true }
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceDto>> Update(int id, [FromBody] UpdateServiceDto updateServiceDto)
        {
            // Walidacja danych
            var validationResult = await _updateValidator.ValidateAsync(updateServiceDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new
                {
                    message = "Dane wejściowe są niepoprawne",
                    errors = errors
                });
            }

            // Zaktualizuj usługę
            var service = await _serviceService.UpdateServiceAsync(id, updateServiceDto);

            if (service == null)
            {
                return NotFound($"Usługa o ID {id} nie znaleziona");
            }

            return Ok(service);
        }

        /// <summary>
        /// Usuwa usługę (soft delete).
        /// DELETE: api/services/1
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _serviceService.DeleteServiceAsync(id);

            if (!success)
            {
                return NotFound($"Usługa o ID {id} nie znaleziona");
            }

            // Zwróć 204 No Content (standard dla DELETE)
            return NoContent();
        }
    }
}

