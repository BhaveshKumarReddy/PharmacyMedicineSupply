using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Repository;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatesScheduleController : ControllerBase
    {
        private readonly IUnitOfWork _uw;
        public DatesScheduleController(IUnitOfWork uw) {
            _uw = uw;
        }

        [HttpGet]
        public async Task<List<DatesSchedule>> GetAllDatesScheduled()
        {
            return await _uw.DatesScheduleRepository.GetAllDatesScheduled();
        }

        [HttpGet("/DateAvailability/{selectedDateString}")]
        public async Task<bool> DateAvailable(string selectedDateString) {
            DateTime selectedDate = Convert.ToDateTime(selectedDateString);
            var available = await _uw.DatesScheduleRepository.CheckAvailability(selectedDate);
            return available;
        }

        [HttpGet("/MarkSupplied/{selectedDateString}")]
        public async Task<DatesSchedule> MarkSupplied(string selectedDateString)
        {
            DateTime selectedDate = Convert.ToDateTime(selectedDateString);
            var date = await _uw.DatesScheduleRepository.UpdateSupply(selectedDate);
            return date;
        }
    }
}
