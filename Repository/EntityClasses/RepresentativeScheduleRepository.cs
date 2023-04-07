using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class RepresentativeScheduleRepository : IRepresentativeScheduleRepository<RepresentativeSchedule>
    {
        private readonly PharmacySupplyContext _db;
        public RepresentativeScheduleRepository(PharmacySupplyContext db)
        {
            _db = db;
        }

        public async Task AddSchedules(List<RepresentativeSchedule> schedules)
        {
           await _db.RepresentativeSchedules.AddRangeAsync(schedules);
           await _db.SaveChangesAsync();
        }

        public async Task UpdateStatus(int id)
        {
            var newSch = _db.RepresentativeSchedules.FirstOrDefault(x => x.Id == id);
            var schedule = _db.RepresentativeSchedules.Find(newSch.RepresentativeName, newSch.DoctorName, newSch.Date);
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
