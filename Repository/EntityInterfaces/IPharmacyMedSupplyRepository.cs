using PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IPharmacyMedSupplyRepository<PharmacyMedSupply>
    {
        Task<IEnumerable<PharmacyMedSupplyDTO>> GetPharmacyMedicineSupply();
        Task AddPharmacyMedSupply(PharmacyMedSupply pharmacyMedicineSupply);
        Task<IEnumerable<PharmacyMedSupplyDTO>> GetPharmacyMedicineSupplyByDate(DateTime startDate);
    }
}
