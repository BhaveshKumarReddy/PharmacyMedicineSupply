using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Repository.Classes;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly PharmacySupplyContext _db;
        public ManagerController(PharmacySupplyContext db)
        {
            _db = db;
        }

        [HttpPost("CheckingEmail")]
        public IActionResult  CheckManagerEmail(string email)
        {
            Manager m1=_db.Managers.Where(x=>x.Email==email).FirstOrDefault();    
            if (m1!=null)
            {
                return Ok(false);
            }
            return Ok(true);
        }
        [HttpPost("CheckingName")]
        public IActionResult CheckManagerName(string name)
        {
            Manager m= _db.Managers.Where(x=>x.Name==name).FirstOrDefault();
            if (m != null)
            {
                return Ok(false);
            }
            return Ok(true);
        }
    }
        
}
