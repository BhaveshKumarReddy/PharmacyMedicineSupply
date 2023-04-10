using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class RepresentativeScheduleRepository : IRepresentativeScheduleRepository<RepresentativeSchedule>
    {
        private readonly PharmacySupplyContext _db;
        private readonly IDatesScheduleRepository<DatesSchedule> _datesScheduleRepo;
        public RepresentativeScheduleRepository(PharmacySupplyContext db, IDatesScheduleRepository<DatesSchedule> datesScheduleRepo)
        {
            _db = db;
            _datesScheduleRepo = datesScheduleRepo;
        }

        public async Task<List<RepresentativeSchedule>> GetScheduleByDate(DateTime startDate)
        {
            DatesSchedule DateSchedule = await _datesScheduleRepo.GetDatesSchedule(startDate);
            var res = await _db.RepresentativeSchedules.FromSqlRaw($"exec GetScheduleByDate @startDate='{startDate.ToString("yyyy-MM-dd")}', @EndDate='{DateSchedule.EndDate.ToString("yyyy-MM-dd")}'").ToListAsync();
            return res;
        }

        public async Task AddSchedules(List<RepresentativeSchedule> schedules)
        {
           await _db.RepresentativeSchedules.AddRangeAsync(schedules);
           await _db.SaveChangesAsync();
        }

        public async Task UpdateStatus(int id)
        {
            var newSchedule = _db.RepresentativeSchedules.FirstOrDefault(x => x.Id == id);
            var schedule = _db.RepresentativeSchedules.Find(newSchedule.RepresentativeName, newSchedule.DoctorName, newSchedule.Date);
            schedule.Status = 1;
            await _db.SaveChangesAsync();
        }

        public async Task<RepresentativeSchedule> GetScheduleById(int id)
        {
            var res = await _db.RepresentativeSchedules.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }
    }
}
