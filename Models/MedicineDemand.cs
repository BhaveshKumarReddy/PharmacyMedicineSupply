using System.ComponentModel.DataAnnotations;

namespace PharmacySupplyProject.Models
{
    public class MedicineDemand
    {
        [Key]
        public string Name { get; set; } = string.Empty;
        public int DemandCount { get; set; }
    }
}
