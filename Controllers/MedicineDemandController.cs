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
    public class MedicineDemandController : ControllerBase
    {
        private readonly IUnitOfWork _uw;
        public MedicineDemandController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [HttpGet("ResetMedicineDemand")]
        public async Task<ActionResult> ResetMedicineDemand()
        {
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
                return Ok();
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

        [HttpPut]
        public async Task<ActionResult<MedicineDemand>> UpdateMedicneDemand(string name, int Demand)
        {
            try
            {
                return await _uw.MedicineDemandRepository.UpdateMedicineDemand(name, Demand);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
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


        [HttpPut("UpdateAllDemands/{repSchedule_ID}")]
        public async Task<ActionResult> UpdateAllMedicineDemand(int repSchedule_ID, List<MedicineDemand> MDUpdateList)
        {
            try
            {
                foreach (var md in MDUpdateList)
                {
                    var x = await _uw.MedicineDemandRepository.UpdateMedicineDemand(md.Name, md.DemandCount);
                }
                await _uw.RepresentativeScheduleRepository.UpdateStatus(repSchedule_ID);
                var date = await _uw.RepresentativeScheduleRepository.GetScheduleById(repSchedule_ID);
                await _uw.DatesScheduleRepository.UpdateCounter(date.Date);
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
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
    }
}
