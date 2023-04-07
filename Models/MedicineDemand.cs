using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace PharmacySupplyProject.Models
{
    public class MedicineDemand
    {
        [Key]
        public string Name { get; set; } = string.Empty;
        public int DemandCount { get; set; }

        
    }
}
