using System.ComponentModel.DataAnnotations;

namespace PharmacySupplyProject.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }

        [Key]
        public string Name { get; set; }=string.Empty;

    }
}
