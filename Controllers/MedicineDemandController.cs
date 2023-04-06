using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineDemandController : ControllerBase
    {
        private readonly IMedicineDemandRepository<MedicineDemand> _demandRepo;
        private readonly IDatesScheduleRepository<DatesSchedule> _datesScheduleRepo;
        private readonly IRepresentativeScheduleRepository<RepresentativeSchedule> _repSchedule;
        private readonly IMedicineStockRepository<MedicineStock> _medicineStockRepo;
        public MedicineDemandController(
            IMedicineDemandRepository<MedicineDemand> demandRepo,
            IMedicineStockRepository<MedicineStock> medicineStockRepo,
            IRepresentativeScheduleRepository<RepresentativeSchedule> repSchedule,
            IDatesScheduleRepository<DatesSchedule> datesScheduleRepo)
        {
            _demandRepo = demandRepo;
            _medicineStockRepo = medicineStockRepo;
            _repSchedule = repSchedule;
            _datesScheduleRepo = datesScheduleRepo;
        }
        [HttpGet]
        public async Task<ActionResult> ResetMedicineDemand()
        {
            IEnumerable<string> Names = await _medicineStockRepo.GetMedicineStocksName();
            foreach(string name in Names)
            {
                MedicineDemand md = new MedicineDemand();
                md.Name = name;
                md.DemandCount = 0;
                await _demandRepo.AddMedicineDemand(md);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<MedicineDemand>> UpdateMedicneDemand(string name, int Demand)
        {
            return await _demandRepo.UpdateMedicineDemand(name, Demand);
        }

        [HttpPut("UpdateAllDemands/{repSchedule_ID}")]
        public async Task<ActionResult> UpdateAllMedicineDemand(int repSchedule_ID, List<MedicineDemand> MDUpdateList)
        {
            foreach(var md in MDUpdateList)
            {
                await _demandRepo.UpdateMedicineDemand(md.Name, md.DemandCount);
            }
            await _repSchedule.UpdateStatus(repSchedule_ID);
            var date = await _repSchedule.GetScheduleById(repSchedule_ID);
            await _datesScheduleRepo.UpdateCounter(date.Date);
            return Ok();
        }
    }
}
