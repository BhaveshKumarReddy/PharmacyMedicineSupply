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
        
        public MedicineStocksController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<MedicineStockResponse>> MedicineStockInformation(int page)
        {
            if (await _uw.MedicineStockRepository.GetMedicineStocks(page) == null)
            {
                return NotFound();
            }
            try
            {
                return await _uw.MedicineStockRepository.GetMedicineStocks(page);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
