using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models.DTO.MedicineStock;
using PharmacyMedicineSupply.Models.DTO.MedicineSupply;
using PharmacyMedicineSupply.Repository;
using PharmacyMedicineSupply.Repository.EntityClasses;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineStocksController : ControllerBase
    {
        private readonly IUnitOfWork _uw;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(MedicineStocksController));
        public MedicineStocksController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<MedicineStockResponse>> MedicineStockInformation(int page)
        {
            _log4net.Info("Medicine Stock page "+page+" Invoked");
            if (await _uw.MedicineStockRepository.GetMedicineStocks(page) == null)
            {
                return NotFound();
            }
            try
            {
                return await _uw.MedicineStockRepository.GetMedicineStocks(page);
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
