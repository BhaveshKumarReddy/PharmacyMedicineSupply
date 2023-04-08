using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Repository.Classes;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepo;
        public ManagerController(IManagerRepository managerRepository)
        {
            _managerRepo = managerRepository;
        }

        [HttpPost("CheckingEmail")]
        public async Task<IActionResult> CheckManagerEmail(string email)
        {
            Manager m1= await _managerRepo.GetManagerbymail(email);
            if (m1!=null)
            {
                return Ok(false);
            }
            return Ok(true);
        }
        [HttpPost("CheckingName")]
        public async Task<IActionResult> CheckManagerName(string name)
        {
            Manager m= await _managerRepo.GetManagerbyname(name);
            if (m != null)
            {
                return Ok(false);
            }
            return Ok(true);
        }
    }
        
}
