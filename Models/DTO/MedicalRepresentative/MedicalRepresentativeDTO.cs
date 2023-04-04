using System.ComponentModel.DataAnnotations;

namespace PharmacyMedicineSupply.Models.DTO.MedicalRepresentative
{
    public class MedicalRepresentativeDTO
    {
        public string Name { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;
    }
}
