﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply;
using PharmacyMedicineSupply.Repository;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyMedSupplyController : ControllerBase
    {
        private readonly IUnitOfWork _uw;

        public PharmacyMedSupplyController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [HttpGet("Supply/{startDateString}")]
        public async Task<IEnumerable<PharmacyMedSupplyDTO>> GetPharmacyMedSupply(string startDateString)
        {
            DateTime startDate = Convert.ToDateTime(startDateString);
            int supply, InStock, demand, PharmacyRecords,FinalStock,Supplied,i;
            List<Pharmacy> ListOfPharmacies = await _uw.PharmacyRepository.GetAllPharmacies();
            var ListOfMedicineDemand = await _uw.MedicineDemandRepository.GetMedicineDemand();
            foreach(var x in ListOfMedicineDemand)
            {
                InStock= (await _uw.MedicineStockRepository.GetStockByMedicineName(x.Name)).NumberOfTabletsInStock;
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
                MedicineStock ms = await _uw.MedicineStockRepository.GetStockByMedicineName(x.Name);
                ms.NumberOfTabletsInStock -= FinalStock;
                await _uw.MedicineStockRepository.UpdateMedicineStock(ms);
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
                    await _uw.PharmacyMedSupplyRepository.AddPharmacyMedSupply(pm);
                }
            } 
            await _uw.MedicineDemandRepository.ResetMedicineDemand();
            await _uw.DatesScheduleRepository.UpdateSupply(startDate);
            return await _uw.PharmacyMedSupplyRepository.GetPharmacyMedicineSupplyByDate(startDate);
        }

        [HttpGet("AlreadySupplied/{startDateString}")]
        public async Task<IEnumerable<PharmacyMedSupplyDTO>> GetAlreadySuppliedPharma(string startDateString)
        {
            DateTime startDate = Convert.ToDateTime(startDateString);
            return await _uw.PharmacyMedSupplyRepository.GetPharmacyMedicineSupplyByDate(startDate);
        }
    }
}
