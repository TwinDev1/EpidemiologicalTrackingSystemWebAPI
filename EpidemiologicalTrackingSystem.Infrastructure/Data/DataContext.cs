using EpidemiologicalTrackingSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EpidemiologicalTrackingSystem.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Individual> Individuals { get; set; }
    }
}
