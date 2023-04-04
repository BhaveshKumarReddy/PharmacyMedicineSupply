using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineStocksController : ControllerBase
    {
        private readonly PharmacySupplyContext _context;

        public MedicineStocksController(PharmacySupplyContext context)
        {
            _context = context;
        }

        // GET: api/MedicineStocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineStock>>> GetMedicineStocks()
        {
          if (_context.MedicineStocks == null)
          {
              return NotFound();
          }
            return await _context.MedicineStocks.ToListAsync();
        }

        
        private bool MedicineStockExists(string id)
        {
            return (_context.MedicineStocks?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
