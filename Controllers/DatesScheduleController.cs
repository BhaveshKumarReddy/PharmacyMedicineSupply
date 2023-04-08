using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<DatesSchedule>>> GetAllDatesScheduled()
        {
            try
            {
                return await _uw.DatesScheduleRepository.GetAllDatesScheduled();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("/DateAvailability/{selectedDateString}")]
        public async Task<ActionResult<bool>> DateAvailable(string selectedDateString) {
            DateTime selectedDate = Convert.ToDateTime(selectedDateString);
            var available = await _uw.DatesScheduleRepository.CheckAvailability(selectedDate);
            return available;
        }
    }
}
