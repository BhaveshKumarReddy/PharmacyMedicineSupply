using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyMedSupplyController : ControllerBase
    {
        private readonly IPharmacyRepository<Pharmacy> _pharmacyRepo;
        private readonly IMedicineDemandRepository<MedicineDemand> _demandRepo;
        private readonly IDatesScheduleRepository<DatesSchedule> _datesScheduleRepo;
        private readonly IMedicineStockRepository<MedicineStock> _medicineStockRepo;
        private readonly IPharmacyMedSupplyRepository<PharmacyMedSupply> _pharmacyMedSupplyRepo;

        public PharmacyMedSupplyController(IMedicineDemandRepository<MedicineDemand> demandRepo, 
            IPharmacyRepository<Pharmacy> pharmacyRepo, 
            IPharmacyMedSupplyRepository<PharmacyMedSupply> pharmacyMedSupplyRepo, 
            IMedicineStockRepository<MedicineStock> medicineStockRepo,
            IDatesScheduleRepository<DatesSchedule> datesScheduleRepo)
        {
            _demandRepo = demandRepo;
            _pharmacyRepo = pharmacyRepo;
            _datesScheduleRepo = datesScheduleRepo;
            _pharmacyMedSupplyRepo = pharmacyMedSupplyRepo;
            _medicineStockRepo = medicineStockRepo;
        }

        [HttpGet("Supply")]
        public async Task<IEnumerable<PharmacyMedSupplyDTO>> GetPharmacyMedSupply(DateTime startDate)
        {
            int supply, InStock, demand, PharmacyRecords,FinalStock,Supplied,i;
            List<Pharmacy> ListOfPharmacies = await _pharmacyRepo.GetAllPharmacies();
            var ListOfMedicineDemand = await _demandRepo.GetMedicineDemand();
            foreach(var x in ListOfMedicineDemand)
            {
                InStock= (await _medicineStockRepo.GetStockByMedicineName(x.Name)).NumberOfTabletsInStock;
                demand = x.DemandCount;
                PharmacyRecords = ListOfPharmacies.Count;                
                if (demand >= InStock)
                {
                    FinalStock = InStock;
                }
                else
                {
                    FinalStock = demand;
                }
                if (FinalStock == 0)
                {
                    continue;
                }
                supply = FinalStock / PharmacyRecords;
                MedicineStock ms = await _medicineStockRepo.GetStockByMedicineName(x.Name);
                ms.NumberOfTabletsInStock -= FinalStock;
                await _medicineStockRepo.UpdateMedicineStock(ms);
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
                    pm.DateTime= startDate;
                    i ++;
                    await _pharmacyMedSupplyRepo.AddPharmacyMedSupply(pm);
                }
            } 
            await _demandRepo.ResetMedicineDemand();
            await _datesScheduleRepo.UpdateSupply(startDate);
            return await _pharmacyMedSupplyRepo.GetPharmacyMedicineSupplyByDate(startDate);
            //stored procedure to clear this table.

        }

        [HttpGet("AlreadySupplied")]
        public Task<IEnumerable<PharmacyMedSupplyDTO>> GetAlreadySuppliedPharma(DateTime startDate)
        {
            return _pharmacyMedSupplyRepo.GetPharmacyMedicineSupplyByDate(startDate);
        }
    }
}
