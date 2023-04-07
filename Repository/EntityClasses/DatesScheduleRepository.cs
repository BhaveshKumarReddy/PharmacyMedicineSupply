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
        public async Task<DatesSchedule> GetDatesSchedule(DateTime startDate)
        {
            var res = await _db.DatesSchedules.FirstOrDefaultAsync(x => x.StartDate == startDate);
            return res;
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

        public async Task UpdateCounter(DateTime repScheduleDate)
        {
            var all_datesScheduled = await GetAllDatesScheduled();
            int total_meets = (new Doctors()).getLength();
            foreach (var date in all_datesScheduled)
            {
                if (repScheduleDate >= date.StartDate && repScheduleDate <= date.EndDate)
                {
                    date.CountCompletedMeets += 1;
                    if(date.CountCompletedMeets >= total_meets)
                    {
                        date.Completed = 1;
                    }
                    _db.DatesSchedules.Update(date);
                    _db.SaveChanges();
                    break;
                }
            }

        }
    }
}
