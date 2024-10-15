using EpidemiologicalTrackingSystem.Core.Entities;

namespace EpidemiologicalTrackingSystem.Core.Interfaces
{
    public interface IIndividualRepository
    {
        Task<IEnumerable<Individual>> GetAllDiagnosedAsync();
        Task<Individual?> GetByIdAsync(int id);
        Task AddAsync(Individual individual);
        Task UpdateAsync(Individual individual);
        Task DeleteAsync(int id);
    }
}
