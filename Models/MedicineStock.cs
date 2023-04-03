using System.ComponentModel.DataAnnotations;

namespace PharmacySupplyProject.Models
{
    public class MedicineStock
    {
        [Key]
        public string Name { get; set; } = string.Empty;
        public string ChemicalComposition { get; set; } = string.Empty;
        public string TargetAilment { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime DateOfExpiry { get; set; }
        public int NumberOfTabletsInStock { get; set; }


    }
}
