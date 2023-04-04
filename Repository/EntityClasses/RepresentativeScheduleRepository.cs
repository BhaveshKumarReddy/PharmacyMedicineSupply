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
    }
}
