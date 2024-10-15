using EpidemiologicalTrackingSystem.Core.Entities;
using EpidemiologicalTrackingSystem.Core.Interfaces;

namespace EpidemiologicalTrackingSystem.Application.Services
{
    public class IndividualService
    {
        private readonly IIndividualRepository _repository;

        public IndividualService(IIndividualRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Individual>> GetAllDiagnosedAsync()
        {
            return await _repository.GetAllDiagnosedAsync();
        }

        public async Task<Individual?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Individual individual)
        {
            await _repository.AddAsync(individual);
        }

        public async Task UpdateAsync(Individual individual)
        {
            await _repository.UpdateAsync(individual);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
