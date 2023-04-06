namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IDatesScheduleRepository<DatesSchedule>
    {
        Task<List<DatesSchedule>> GetAllDatesScheduled();
        Task<bool> CheckAvailability(DateTime selectedDate);
        Task<DatesSchedule> AddDateSchedule(DatesSchedule newSchedule);
        Task<DatesSchedule> UpdateSupply(DateTime date);
        Task UpdateCounter(DateTime repScheduleDate);
    }
}
