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
            await _db.MedicineDemands.AddAsync(MedicineDemand);
            await _db.SaveChangesAsync();
            return await _db.MedicineDemands.FindAsync(MedicineDemand.Name);
        }

        public void DeleteMedicineDemand(string MedicineName)
        {
            MedicineDemand md= _db.MedicineDemands.Find(MedicineName);
            _db.MedicineDemands.Remove(md);
            _db.SaveChanges() ;
        }

        public async Task<List<MedicineDemand>> GetMedicineDemand()
        {
            return await _db.MedicineDemands.ToListAsync();
            
        }

        public async Task<MedicineDemand> UpdateMedicineDemand(string MedicineName, int count)
        {
            MedicineDemand md= await _db.MedicineDemands.Where(x=>x.Name==MedicineName).SingleOrDefaultAsync();
            md.DemandCount+=count;
            _db.MedicineDemands.Update(md);
            await _db.SaveChangesAsync();
            return await _db.MedicineDemands.FindAsync(MedicineName);

        }
        public async Task ResetMedicineDemand()
        {
            var MedicineDemand = await _db.MedicineDemands.ToListAsync();
            foreach(var v in  MedicineDemand)
            {
                v.DemandCount = 0;
                _db.MedicineDemands.Update(v);
                await _db.SaveChangesAsync();
            }

        }
        
       
    }
}
