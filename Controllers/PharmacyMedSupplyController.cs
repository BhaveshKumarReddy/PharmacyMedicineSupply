using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Models.DTO.MedicineStock;
using PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply;
using PharmacyMedicineSupply.Repository;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;
using System.Collections;

namespace PharmacyMedicineSupply.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyMedSupplyController : ControllerBase
    {
        private readonly IUnitOfWork _uw;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PharmacyMedSupplyController));
        public PharmacyMedSupplyController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [HttpGet("Supply/{page}/{startDateString}")]
        public async Task<ActionResult<PharmacyMedSupplyResponse>> GetPharmacyMedSupply(int page, string startDateString)
        {
            _log4net.Info("Creating pharmacy medicine supply is Invoked");
            try
            {
                DateTime startDate = Convert.ToDateTime(startDateString);
                int supply, InStock, demand, PharmacyRecords, FinalStock, RemSupply;
                List<Pharmacy> ListOfPharmacies = await _uw.PharmacyRepository.GetAllPharmacies();
                var ListOfMedicineDemand = await _uw.MedicineDemandRepository.GetMedicineDemand();
                foreach (var x in ListOfMedicineDemand)
                {
                    InStock = (await _uw.MedicineStockRepository.GetStockByMedicineName(x.Name)).NumberOfTabletsInStock;
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
                    RemSupply = FinalStock - (supply * PharmacyRecords);
                    foreach (Pharmacy p in ListOfPharmacies)
                    {
                        PharmacyMedSupply pm = new PharmacyMedSupply();
                        pm.PharmacyName = p.Name;
                        pm.MedicineName = x.Name;
                        pm.SupplyCount = supply;
                        if (RemSupply > 0) 
                        {
                            pm.SupplyCount += 1;
                            RemSupply -= 1;
                        }
                        pm.DateTime = startDate;
                        await _uw.PharmacyMedSupplyRepository.AddPharmacyMedSupply(pm);
                    }
                }
                await _uw.MedicineDemandRepository.ResetMedicineDemand();
                await _uw.DatesScheduleRepository.UpdateSupply(startDate);
                _log4net.Info("Successfully created pharmacy medicine supply");
                return await _uw.PharmacyMedSupplyRepository.GetPharmacyMedicineSupplyByDate(page, startDate);
            }
            catch (DbUpdateException ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AlreadySupplied/{page}/{startDateString}")]
        public async Task<ActionResult<PharmacyMedSupplyResponse>> GetAlreadySuppliedPharma(int page, string startDateString)
        {
            _log4net.Info("Getting pharmacy supply table is Invoked");
            try
            {
                DateTime startDate = Convert.ToDateTime(startDateString);
                return await _uw.PharmacyMedSupplyRepository.GetPharmacyMedicineSupplyByDate(page, startDate);
            }
            catch (DbUpdateException ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log4net.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
