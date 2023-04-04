using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyMedSupplyController : ControllerBase
    {
        private readonly IMedicineDemandRepository<MedicineDemand> _demandRepo;
        private readonly IPharmacyRepository<Pharmacy> _pharmacyRepo;
        private readonly IPharmacyMedSupplyRepository<PharmacyMedSupply> _pharmacyMedSupplyRepo;
        private readonly IMedicineStockReposiroty<MedicineStock> _medicineStockRepo;
        public PharmacyMedSupplyController(IMedicineDemandRepository<MedicineDemand> demandRepo, IPharmacyRepository<Pharmacy> pharmacyRepo, IPharmacyMedSupplyRepository<PharmacyMedSupply> pharmacyMedSupplyRepo, IMedicineStockReposiroty<MedicineStock> medicineStockRepo)
        {
            _demandRepo = demandRepo;
            _pharmacyRepo = pharmacyRepo;
            _pharmacyMedSupplyRepo = pharmacyMedSupplyRepo;
            _medicineStockRepo = medicineStockRepo;
        }

        [HttpGet]
        public Task<IEnumerable<PharmacyMedSupply>> GetPharmacyMedSupply()
        {
            int supply, InStock, demand, PharmacyRecords;
            List<Pharmacy> ListOfPharmacies = _pharmacyRepo.GetAllPharmacies();
            var ListOfMedicineDemand =_demandRepo.GetMedicineDemand();
            foreach(var x in ListOfMedicineDemand)
            {
                InStock= _medicineStockRepo.GetStockByMedicineName(x.Name).NumberOfTabletsInStock;
                demand = x.DemandCount;
                PharmacyRecords = _pharmacyRepo.GetAllPharmacies().Count;
                if (demand >= InStock)
                {
                    supply = InStock / PharmacyRecords;
                    MedicineStock ms = _medicineStockRepo.GetStockByMedicineName(x.Name);
                    ms.NumberOfTabletsInStock = 0;
                    _medicineStockRepo.UpdateMedicineStock(ms);
                }
                else
                {
                    supply = demand/ PharmacyRecords;
                    MedicineStock ms = _medicineStockRepo.GetStockByMedicineName(x.Name);
                    ms.NumberOfTabletsInStock-= demand;
                    _medicineStockRepo.UpdateMedicineStock(ms);
                }

                foreach (Pharmacy p in ListOfPharmacies)
                {
                    PharmacyMedSupply pm = new PharmacyMedSupply();
                    pm.PharmacyName = p.Name;
                    pm.MedicineName = x.Name;
                    pm.SupplyCount = supply;
                    pm.DateTime= DateTime.Now;
                    _pharmacyMedSupplyRepo.AddPharmacyMedSupply(pm);
                }

            }
            
            return _pharmacyMedSupplyRepo.GetPharmacyMedicineSupply();

        }
    }
}
