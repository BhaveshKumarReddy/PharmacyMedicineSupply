using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models.DTO.MedicineSupply;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineStocksController : ControllerBase
    {
        private readonly PharmacySupplyContext _context;
        private readonly IMapper _mapper;

        public MedicineStocksController(PharmacySupplyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineStockDTO>>> MedicineStockInformation()
        {
            if (_context.MedicineStocks == null)
            {
                return NotFound();
            }
            return await _context.MedicineStocks.Select(x => _mapper.Map<MedicineStockDTO>(x)).ToListAsync();
        }
    }
}
