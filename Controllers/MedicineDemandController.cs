using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineDemandController : ControllerBase
    {
        private readonly IMedicineDemandRepository<MedicineDemand> _demandRepo;
        private readonly IMedicineStockReposiroty<MedicineStock> _medicineStockRepo;
        public MedicineDemandController(IMedicineDemandRepository<MedicineDemand> demandRepo,IMedicineStockReposiroty<MedicineStock> medicineStockRepo)
        {
            _demandRepo = demandRepo;
            _medicineStockRepo = medicineStockRepo;
        }
        [HttpGet]
        public async Task<ActionResult> ResetMedicineDemand()
        {
            IEnumerable<string> Names = _medicineStockRepo.GetMedicineStocksName();
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


    }
}
