using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatesScheduleController : ControllerBase
    {
        private readonly IDatesScheduleRepository<DatesSchedule> _datesScheduleRepo;
        public DatesScheduleController(IDatesScheduleRepository<DatesSchedule> datesScheduleRepo) {
            _datesScheduleRepo = datesScheduleRepo;
        }

        [HttpGet]
        public async Task<List<DatesSchedule>> GetAllDatesScheduled()
        {
            return await _datesScheduleRepo.GetAllDatesScheduled();
        }

        [HttpGet("/DateAvailability/{selectedDateString}")]
        public async Task<bool> DateAvailable(string selectedDateString) {
            DateTime selectedDate = Convert.ToDateTime(selectedDateString);
            var available = await _datesScheduleRepo.CheckAvailability(selectedDate);
            return available;
        }

        [HttpGet("/MarkSupplied/{selectedDateString}")]
        public async Task<DatesSchedule> MarkSupp(string selectedDateString)
        {
            DateTime selectedDate = Convert.ToDateTime(selectedDateString);
            var date = await _datesScheduleRepo.UpdateSupply(selectedDate);
            return date;
        }
    }
}
