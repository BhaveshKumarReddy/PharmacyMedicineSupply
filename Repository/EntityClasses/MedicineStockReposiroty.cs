using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models.DTO.MedicineSupply;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class MedicineStockRepository : IMedicineStockRepository<MedicineStock>
    {
        private readonly PharmacySupplyContext _db;
        private readonly IMapper _mapper;
        public MedicineStockRepository(PharmacySupplyContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<string> GetMedicineForSchedule(string ailment)
        {
            var medicines = await _db.MedicineStocks.Where(x => x.TargetAilment == ailment).ToListAsync();
            string all_medicines = "";
            foreach(var medicine in medicines)
            {
                all_medicines += ","+medicine.Name;
            }
            return (all_medicines.Length>0)? all_medicines.Substring(1): all_medicines;
        }

        public async Task<MedicineStock> GetStockByMedicineName(string medicinename)
        {
            return await _db.MedicineStocks.FirstOrDefaultAsync(x => x.Name == medicinename);
        }

        public async Task UpdateMedicineStock(MedicineStock medicineStock)
        {
            _db.MedicineStocks.Update(medicineStock);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<String>> GetMedicineStocksName()
        {            
            return await _db.MedicineStocks.Select(x=>x.Name).ToListAsync();
        }
        public async Task<List<MedicineStockDTO>> GetMedicineStocks()
        {
            return await _db.MedicineStocks.Select(x => _mapper.Map<MedicineStockDTO>(x)).ToListAsync();
        }

    }
}
