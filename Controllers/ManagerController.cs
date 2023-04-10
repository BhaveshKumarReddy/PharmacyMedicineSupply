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
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ManagerController));
        private readonly IUnitOfWork _uw;
        public ManagerController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        [AllowAnonymous]

        [HttpPost("CheckingEmail")]
        public async Task<IActionResult> CheckManagerEmail(string email)
        {
            _log4net.Info("Check Email Invoked");
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

        [AllowAnonymous]
        [HttpPost("CheckingName")]
        public async Task<IActionResult> CheckManagerName(string name)
        {
            _log4net.Info("Check name Invoked");
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
