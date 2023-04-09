using PharmacyMedicineSupply.Models.DTO.MedicineSupply;

namespace PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply
{
    public class PharmacyMedSupplyResponse
    {
        public List<PharmacyMedSupplyDTO> pharmacyMedSupplies { get; set; } = new List<PharmacyMedSupplyDTO>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
