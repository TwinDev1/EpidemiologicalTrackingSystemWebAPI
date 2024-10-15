using EpidemiologicalTrackingSystem.Application.Services;
using EpidemiologicalTrackingSystem.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EpidemiologicalTrackingSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualsController : ControllerBase
    {
        private readonly IndividualService _service;
        private readonly ILogger<IndividualsController> _logger;

        public IndividualsController(IndividualService service, ILogger<IndividualsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/individuals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Individual>>> GetIndividuals()
        {
            _logger.LogInformation("Fetching all diagnosed individuals.");

            var individuals = await _service.GetAllDiagnosedAsync();

            if (individuals == null || !individuals.Any())
            {
                _logger.LogWarning("No diagnosed individuals found.");
                return NotFound(new { Message = "No individuals diagnosed." });
            }

            _logger.LogInformation($"Found {individuals.Count()} diagnosed individuals.");
            return Ok(individuals);
        }

        // GET: api/individuals/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIndividual(int id)
        {
            _logger.LogInformation($"Fetching individual with ID: {id}");

            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID provided.");
                return BadRequest(new { Message = "Invalid ID provided." });
            }

            var individual = await _service.GetByIdAsync(id);
            if (individual == null)
            {
                _logger.LogWarning($"Individual with ID {id} not found.");
                return NotFound(new { Message = $"Individual with ID {id} not found." });
            }

            _logger.LogInformation($"Individual with ID {id} found.");
            return Ok(individual);
        }

        // POST: api/individuals
        [HttpPost]
        public async Task<IActionResult> CreateIndividual([FromBody] Individual individual)
        {
            _logger.LogInformation("Creating new individual.");

            if (individual == null)
            {
                _logger.LogWarning("No data provided for individual.");
                return BadRequest(new { Message = "Individual data is required." });
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for individual.");
                return BadRequest(ModelState);
            }

            if (!individual.Diagnosed || individual.DateDiagnosed == null)
            {
                _logger.LogWarning("Individual must be diagnosed and have a diagnosis date.");
                return BadRequest(new { Message = "Individual must be diagnosed and have a diagnosis date." });
            }

            await _service.AddAsync(individual);
            _logger.LogInformation($"Individual with ID {individual.Id} created successfully.");
            return CreatedAtAction(nameof(GetIndividual), new { id = individual.Id }, new { Message = $"Individual with ID {individual.Id} was created successfully." });
        }

        // PUT: api/individuals/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIndividual(int id, [FromBody] Individual individual)
        {
            _logger.LogInformation($"Updating individual with ID: {id}");

            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID provided.");
                return BadRequest(new { Message = "Invalid ID provided." });
            }

            if (individual == null)
            {
                _logger.LogWarning("No data provided for update.");
                return BadRequest(new { Message = "Individual data is required." });
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for individual.");
                return BadRequest(ModelState);
            }

            if (id != individual.Id)
            {
                _logger.LogWarning("ID mismatch between URL and body.");
                return BadRequest(new { Message = "ID mismatch between URL and body." });
            }

            var existingIndividual = await _service.GetByIdAsync(id);
            if (existingIndividual == null)
            {
                _logger.LogWarning($"Individual with ID {id} not found.");
                return NotFound(new { Message = $"Individual with ID {id} not found." });
            }

            await _service.UpdateAsync(individual);
            _logger.LogInformation($"Individual with ID {individual.Id} updated successfully.");
            return Ok(new { Message = $"Individual with ID {individual.Id} was updated successfully." });
        }

        // DELETE: api/individuals/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndividual(int id)
        {
            _logger.LogInformation($"Deleting individual with ID: {id}");

            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID provided.");
                return BadRequest(new { Message = "Invalid ID provided." });
            }

            var existingIndividual = await _service.GetByIdAsync(id);
            if (existingIndividual == null)
            {
                _logger.LogWarning($"Individual with ID {id} not found.");
                return NotFound(new { Message = $"Individual with ID {id} not found." });
            }

            await _service.DeleteAsync(id);
            _logger.LogInformation($"Individual with ID {id} deleted successfully.");
            return Ok(new { Message = $"Individual with ID {id} was deleted successfully." });
        }
    }
}
