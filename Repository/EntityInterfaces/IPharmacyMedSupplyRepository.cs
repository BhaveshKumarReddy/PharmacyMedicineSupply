using PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IPharmacyMedSupplyRepository<PharmacyMedSupply>
    {
        Task<IEnumerable<PharmacyMedSupplyDTO>> GetPharmacyMedicineSupply();
        void AddPharmacyMedSupply(PharmacyMedSupply pharmacyMedicineSupply);
    }
}
