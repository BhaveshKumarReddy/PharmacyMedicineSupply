using PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IPharmacyMedSupplyRepository<PharmacyMedSupply>
    {
        Task<List<PharmacyMedSupplyDTO>> GetPharmacyMedicineSupply();
        Task AddPharmacyMedSupply(PharmacyMedSupply pharmacyMedicineSupply);
        Task<PharmacyMedSupplyResponse> GetPharmacyMedicineSupplyByDate(int page, DateTime startDate);
    }
}
