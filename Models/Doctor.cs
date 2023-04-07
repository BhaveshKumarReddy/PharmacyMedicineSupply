using System.ComponentModel.DataAnnotations;

namespace PharmacySupplyProject.Models
{
    public class Doctor
    {
        public string Name { get; set; } = string.Empty;

        [StringLength(10)]
        public string ContactNumber { get; set; } = string.Empty;

        public string TreatingAilment { get; set; } = string.Empty;
    }
}
