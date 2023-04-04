using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class MedicineDemandRepository : IMedicineDemandRepository<MedicineDemand>
    {
        private readonly PharmacySupplyContext _db;
        public MedicineDemandRepository(PharmacySupplyContext db) 
        {
            _db = db;
        }

        public async Task<MedicineDemand> AddMedicineDemand(MedicineDemand MedicineDemand)
        {
            _db.MedicineDemands.Add(MedicineDemand);
            _db.SaveChanges();
            return await _db.MedicineDemands.FindAsync(MedicineDemand.Name);
        }

        public void DeleteMedicineDemand(string MedicineName)
        {
            MedicineDemand md= _db.MedicineDemands.Find(MedicineName);
            _db.MedicineDemands.Remove(md);
            _db.SaveChanges() ;
        }

        public async Task<IEnumerable<MedicineDemand>> GetMedicineDemand()
        {
            return await _db.MedicineDemands.ToListAsync();
            
        }

        public async Task<MedicineDemand> UpdateMedicineDemand(string MedicineName)
        {
            MedicineDemand md=_db.MedicineDemands.Find(MedicineName);
            _db.MedicineDemands.Update(md);
            _db.SaveChanges();
            return await _db.MedicineDemands.FindAsync(MedicineName);

        }
    }
}
