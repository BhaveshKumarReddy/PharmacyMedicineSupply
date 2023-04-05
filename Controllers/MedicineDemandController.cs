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
        private readonly IPharmacyRepository<Pharmacy> _pharmacyRepo;
        private readonly IPharmacyMedSupplyRepository<PharmacyMedSupply> _pharmacyMedSupplyRepo;
        private readonly IMedicineStockReposiroty<MedicineStock> _medicineStockRepo;
        public MedicineDemandController(IMedicineDemandRepository<MedicineDemand> demandRepo,
            IPharmacyRepository<Pharmacy> pharmacyRepo,
            IPharmacyMedSupplyRepository<PharmacyMedSupply> pharmacyMedSupplyRepo,
            IMedicineStockReposiroty<MedicineStock> medicineStockRepo)
        {
            _demandRepo = demandRepo;
            _pharmacyRepo = pharmacyRepo;
            _pharmacyMedSupplyRepo = pharmacyMedSupplyRepo;
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
                _demandRepo.AddMedicineDemand(md);
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
