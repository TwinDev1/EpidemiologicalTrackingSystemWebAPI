using EpidemiologicalTrackingSystem.Core.Entities;
using EpidemiologicalTrackingSystem.Core.Interfaces;
using EpidemiologicalTrackingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EpidemiologicalTrackingSystem.Infrastructure.Repositories
{
    public class IndividualRepository : IIndividualRepository
    {
        private readonly DataContext _context;

        public IndividualRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Individual>> GetAllDiagnosedAsync()
        {
            return await _context.Individuals.Where(i => i.Diagnosed).ToListAsync();
        }

        public async Task<Individual?> GetByIdAsync(int id)
        {
            return await _context.Individuals.FindAsync(id);
        }

        public async Task AddAsync(Individual individual)
        {
            await _context.Individuals.AddAsync(individual);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Individual individual)
        {
            var existingEntity = await _context.Individuals.FindAsync(individual.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached; 
            }

            _context.Individuals.Update(individual);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var individual = await _context.Individuals.FindAsync(id);
            if (individual != null)
            {
                _context.Individuals.Remove(individual);
                await _context.SaveChangesAsync();
            }
        }
    }
}
