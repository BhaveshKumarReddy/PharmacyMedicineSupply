using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class DatesScheduleRepository: IDatesScheduleRepository<DatesSchedule>
    {
        private readonly PharmacySupplyContext _db;
        public DatesScheduleRepository(PharmacySupplyContext db) {
            _db = db;
        }

        public async Task<List<DatesSchedule>> GetAllDatesScheduled()
        {
            return await _db.DatesSchedules.ToListAsync();
        }

        public async Task<bool> CheckAvailability(DateTime selectedDate)
        {
            var all_datesScheduled = await GetAllDatesScheduled();
            bool available = true;
            foreach(var date in all_datesScheduled)
            {
                if(selectedDate >= date.StartDate && selectedDate <= date.EndDate)
                {
                    available = false;
                    break;
                }
            }
            return available;
        }

        public async Task<DatesSchedule> AddDateSchedule(DatesSchedule newSchedule) {
            await _db.DatesSchedules.AddAsync(newSchedule);
            await _db.SaveChangesAsync();
            return newSchedule;
        }

        public async Task<DatesSchedule> UpdateSupply(DateTime date)
        {
            var newDate = await _db.DatesSchedules.FindAsync(date);
            newDate.Supplied = 1;
            await _db.SaveChangesAsync();
            return newDate;
        }
    }
}
