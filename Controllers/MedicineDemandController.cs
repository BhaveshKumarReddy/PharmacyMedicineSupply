using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public class MedicineDemandController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineDemandController));
        private readonly IUnitOfWork _uw;
        public MedicineDemandController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [AllowAnonymous]
        [HttpGet("ResetMedicineDemand")]
        public async Task<ActionResult> ResetMedicineDemand()
        {
            _log4net.Info("Reset Medicine Demand Invoked");
            try
            {
                IEnumerable<string> Names = await _uw.MedicineStockRepository.GetMedicineStocksName();
                foreach (string name in Names)
                {
                    MedicineDemand md = new MedicineDemand();
                    md.Name = name;
                    md.DemandCount = 0;
                    await _uw.MedicineDemandRepository.AddMedicineDemand(md);
                }
                _log4net.Info("Reset Successful");
                return Ok();
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

        [HttpPut]
        public async Task<ActionResult<MedicineDemand>> UpdateMedicneDemand(string name, int Demand)
        {
            _log4net.Info("Update Medicine Demand Invoked");
            try
            {
                return await _uw.MedicineDemandRepository.UpdateMedicineDemand(name, Demand);
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


        [HttpPut("UpdateAllDemands/{repSchedule_ID}")]
        public async Task<ActionResult> UpdateAllMedicineDemand(int repSchedule_ID, List<MedicineDemand> MDUpdateList)
        {
            _log4net.Info("Update Demand with list Invoked");
            try
            {
                foreach (var md in MDUpdateList)
                {
                    var x = await _uw.MedicineDemandRepository.UpdateMedicineDemand(md.Name, md.DemandCount);
                }
                await _uw.RepresentativeScheduleRepository.UpdateStatus(repSchedule_ID);
                var date = await _uw.RepresentativeScheduleRepository.GetScheduleById(repSchedule_ID);
                await _uw.DatesScheduleRepository.UpdateCounter(date.Date);
                _log4net.Info("Updated Demand, Representative schedule status and Dates schedule counter");
                return Ok();
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
    }
}
