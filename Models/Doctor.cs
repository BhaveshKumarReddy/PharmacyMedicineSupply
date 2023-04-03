using System.ComponentModel.DataAnnotations;

namespace PharmacySupplyProject.Models
{
    public class Doctor
    {
        public string Name { get; set; }

        [StringLength(10)]
        public string ContactNumber { get; set; }

        public string TreatingAilment { get; set; }
    }
}
