using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply;
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
        public PharmacyMedSupplyController(IMedicineDemandRepository<MedicineDemand> demandRepo, 
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
        public Task<IEnumerable<PharmacyMedSupplyDTO>> GetPharmacyMedSupply()
        {
            int supply, InStock, demand, PharmacyRecords,FinalStock,Supplied,i;
            List<Pharmacy> ListOfPharmacies = _pharmacyRepo.GetAllPharmacies();
            var ListOfMedicineDemand =_demandRepo.GetMedicineDemand();
            foreach(var x in ListOfMedicineDemand)
            {
                InStock= _medicineStockRepo.GetStockByMedicineName(x.Name).NumberOfTabletsInStock;
                demand = x.DemandCount;
                PharmacyRecords = _pharmacyRepo.GetAllPharmacies().Count;
                if (demand == 0)
                {
                    continue;
                }
                if (demand >= InStock)
                {
                    FinalStock = InStock;
                }
                else
                {
                    FinalStock = demand;
                }
                supply = FinalStock / PharmacyRecords;
                MedicineStock ms = _medicineStockRepo.GetStockByMedicineName(x.Name);
                ms.NumberOfTabletsInStock -= FinalStock;
                _medicineStockRepo.UpdateMedicineStock(ms);
                Supplied = 0;
                i = 1;
                foreach (Pharmacy p in ListOfPharmacies)
                {
                    PharmacyMedSupply pm = new PharmacyMedSupply();
                    pm.PharmacyName = p.Name;
                    pm.MedicineName = x.Name;
                    pm.SupplyCount = supply;
                    Supplied += supply;
                    if (i == PharmacyRecords)
                    {
                        pm.SupplyCount += (FinalStock - Supplied);
                    }
                    pm.DateTime= DateTime.Now;
                    i ++;
                    _pharmacyMedSupplyRepo.AddPharmacyMedSupply(pm);
                }
            }            
            return _pharmacyMedSupplyRepo.GetPharmacyMedicineSupply();

        }
    }
}
