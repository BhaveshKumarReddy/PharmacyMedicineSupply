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
            IEnumerable<string> Names = await _uw.MedicineStockRepository.GetMedicineStocksName();
            foreach(string name in Names)
            {
                MedicineDemand md = new MedicineDemand();
                md.Name = name;
                md.DemandCount = 0;
                await _uw.MedicineDemandRepository.AddMedicineDemand(md);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<MedicineDemand>> UpdateMedicneDemand(string name, int Demand)
        {
            return await _uw.MedicineDemandRepository.UpdateMedicineDemand(name, Demand);
        }


        [HttpPut("UpdateAllDemands/{repSchedule_ID}")]
        public async Task<ActionResult> UpdateAllMedicineDemand(int repSchedule_ID, List<MedicineDemand> MDUpdateList)
        {
            foreach(var md in MDUpdateList)
            {
                var x = await _uw.MedicineDemandRepository.UpdateMedicineDemand(md.Name, md.DemandCount);
            }
            await _uw.RepresentativeScheduleRepository.UpdateStatus(repSchedule_ID);
            var date = await _uw.RepresentativeScheduleRepository.GetScheduleById(repSchedule_ID);
            await _uw.DatesScheduleRepository.UpdateCounter(date.Date);
            return Ok();
        }
    }
}
