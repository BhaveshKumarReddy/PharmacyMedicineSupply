using System.Threading.Tasks;

namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IRepresentativeScheduleRepository<RepresentativeSchedule>
    {
        Task AddSchedules(List<RepresentativeSchedule> schedules);
        Task UpdateStatus(int id);
        Task<RepresentativeSchedule> GetScheduleById(int id);
        Task<List<RepresentativeSchedule>> GetScheduleByDate(DateTime startDate);
    }
}
