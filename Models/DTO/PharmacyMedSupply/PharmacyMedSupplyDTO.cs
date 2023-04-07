namespace PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply
{
    public class PharmacyMedSupplyDTO
    {
        public string PharmacyName { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public int SupplyCount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
