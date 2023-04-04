namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IRepresentativeScheduleRepository<RepresentativeSchedule>
    {
        Task AddSchedules(List<RepresentativeSchedule> schedules);
    }
}
