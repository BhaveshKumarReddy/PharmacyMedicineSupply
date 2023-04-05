using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IPharmacyMedSupplyRepository<PharmacyMedSupply>
    {
        Task<IEnumerable<PharmacyMedSupply>> GetPharmacyMedicineSupply();
        void AddPharmacyMedSupply(PharmacyMedSupply pharmacyMedicineSupply);
    }
}
