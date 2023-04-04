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
        public List<Pharmacy> GetAllPharmacies()
        {
            return  _db.Pharmacies.ToList();
        }
        

    }
}
