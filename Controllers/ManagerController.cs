using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Repository;
using PharmacyMedicineSupply.Repository.Classes;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IUnitOfWork _uw;
        public ManagerController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [AllowAnonymous]

        [HttpPost("CheckingEmail")]
        public async Task<IActionResult> CheckManagerEmail(string email)
        {
            try
            {
                Manager m1 = await _uw.ManagerRepository.GetManagerbymail(email);
                if (m1 != null)
                {
                    return Ok(false);
                }
                return Ok(true);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
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

        [AllowAnonymous]
        [HttpPost("CheckingName")]
        public async Task<IActionResult> CheckManagerName(string name)
        {
            try
            {
                Manager m = await _uw.ManagerRepository.GetManagerbyname(name);
                if (m != null)
                {
                    return Ok(false);
                }
                return Ok(true);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
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
