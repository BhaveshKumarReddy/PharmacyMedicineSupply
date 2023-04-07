using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class PharmacyRepository : IPharmacyRepository<Pharmacy>
    {
        private readonly PharmacySupplyContext _db;
        public PharmacyRepository(PharmacySupplyContext db)
        {
            _db = db;
        }
        public async Task<List<Pharmacy>> GetAllPharmacies()
        {
            return await _db.Pharmacies.ToListAsync();
        }
        

    }
}
