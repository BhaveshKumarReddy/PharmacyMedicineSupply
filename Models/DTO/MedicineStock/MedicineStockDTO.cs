using System.ComponentModel.DataAnnotations;

namespace PharmacyMedicineSupply.Models.DTO.MedicineSupply
{
    public class MedicineStockDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ChemicalComposition { get; set; } = string.Empty;
        public string TargetAilment { get; set; } = string.Empty;
        public int NumberOfTabletsInStock { get; set; }
    }
}
