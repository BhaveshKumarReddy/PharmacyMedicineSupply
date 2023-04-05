using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class MedicineStockReposiroty : IMedicineStockReposiroty<MedicineStock>
    {
        private readonly PharmacySupplyContext _db;
        public MedicineStockReposiroty(PharmacySupplyContext db)
        {
            _db = db;
        }

        public async Task<string> GetMedicineForSchedule(string ailment)
        {
            var medicines = await _db.MedicineStocks.Where(x => x.TargetAilment == ailment).ToListAsync();
            string all_medicines = "";
            foreach(var medicine in medicines)
            {
                all_medicines += ", "+medicine.Name;
            }
            return (all_medicines.Length>0)? all_medicines.Substring(2): all_medicines;
        }

        public MedicineStock GetStockByMedicineName(string medicinename)
        {
            return _db.MedicineStocks.FirstOrDefault(x => x.Name == medicinename);
        }

        public void UpdateMedicineStock(MedicineStock medicineStock)
        {
            _db.MedicineStocks.Update(medicineStock);
            _db.SaveChanges();
        }


    }
}
