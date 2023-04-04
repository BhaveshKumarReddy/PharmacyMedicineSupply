using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models.DTO.ManagerDTO;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.Classes
{
    public class ManagerRepository : Repository<Manager>, IManagerRepository
    {
        private readonly PharmacySupplyContext _db;
        public ManagerRepository(PharmacySupplyContext db): base(db) {
           _db = db;
        }

        public async Task<Manager> AddManager(Manager manager)
        {
            await _db.Managers.AddAsync(manager);
            await _db.SaveChangesAsync();
            return manager;
        }

        public async Task<Manager> GetManager(ManagerLoginDTO manager)
        {
            return await _db.Managers.SingleOrDefaultAsync(x => x.Email == manager.Email);
        }

    }
}
