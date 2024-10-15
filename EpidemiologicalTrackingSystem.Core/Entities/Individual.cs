using System.ComponentModel.DataAnnotations;

namespace EpidemiologicalTrackingSystem.Core.Entities
{
    public class Individual
    {
        public int Id { get; set; } 

        public string Name { get; set; } = string.Empty; 

        public int Age { get; set; } 

        public List<string> Symptoms { get; set; } = new(); 

        public bool Diagnosed { get; set; } 

        public DateTime? DateDiagnosed { get; set; } 
    }
}
