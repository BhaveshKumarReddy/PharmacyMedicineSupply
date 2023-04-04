using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class PharmacyMedSupplyRepository : IPharmacyMedSupplyRepository<PharmacyMedSupply>
    {
        private readonly PharmacySupplyContext _db;
        public PharmacyMedSupplyRepository(PharmacySupplyContext db)
        {
            _db = db;
        }
        public void AddPharmacyMedSupply(PharmacyMedSupply pharmacyMedicineSupply)
        {
            _db.PharmacyMedicineSupplies.Add(pharmacyMedicineSupply);
 
        }

        public async Task<IEnumerable<PharmacyMedSupply>> GetPharmacyMedicineSupply()
        {
            return await _db.PharmacyMedicineSupplies.ToListAsync();
        }
    }
}
