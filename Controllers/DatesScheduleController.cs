using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DatesScheduleController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(DatesScheduleController));
        private readonly IUnitOfWork _uw;
        public DatesScheduleController(IUnitOfWork uw) {
            _uw = uw;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatesSchedule>>> GetAllDatesScheduled()
        {
            _log4net.Info("Dates Schedules is invoked");
            try
            {
                return await _uw.DatesScheduleRepository.GetAllDatesScheduled();
            }
            catch (DbUpdateException)
            {
                _log4net.Error("Cannot update Database");
                return BadRequest("Cannot update Database");
            }
            catch (SqlException)
            {
                _log4net.Error("Cannot access Database");
                return BadRequest("Cannot access Database");
            }
            catch (NullReferenceException)
            {
                _log4net.Error("Object not found");
                return BadRequest("Object not found");
            }
            catch (Exception ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/DateAvailability/{selectedDateString}")]
        public async Task<ActionResult<bool>> DateAvailable(string selectedDateString) {
            _log4net.Info("Selecting Dates Schedule by date is invoked");
            DateTime selectedDate = Convert.ToDateTime(selectedDateString);
            try
            {
                var available = await _uw.DatesScheduleRepository.CheckAvailability(selectedDate);
                _log4net.Info("Representative schedule retrieved successfully");
                return available;
            }
            catch (DbUpdateException)
            {
                _log4net.Error("Cannot update Database");
                return BadRequest("Cannot update Database");
            }
            catch (SqlException ex)
            {
                _log4net.Error("Cannot access Database");
                return BadRequest("Cannot access Database");
            }
            catch (NullReferenceException ex)
            {
                _log4net.Error("Object not found");
                return BadRequest("Object not found");
            }
            catch (Exception ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
