using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Models.DTO;
using PharmacyMedicineSupply.Models.DTO.ManagerDTO;
using PharmacyMedicineSupply.Repository;
using PharmacyMedicineSupply.Repository.Classes;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PharmacyMedicineSupply.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _config; 
        private readonly IUnitOfWork _uw;
        public AuthorizationController(IConfiguration config, IUnitOfWork uw)
        {
            _config = config;
            _uw = uw;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(ManagerRegisterDTO _manager)
        {
            Manager newManager = new Manager()
            {
                Name = _manager.Name,
                Email = _manager.Email,
                Password = _manager.Password
            };
            newManager.salt = RandomNumberGenerator.GetBytes(128 / 8);
            newManager.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password: newManager.Password,
                 salt: newManager.salt,
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 1000,
                 numBytesRequested: 256/8
            ));
            var res = await _uw.Manager.CreateAsync(newManager);
            var tokenString = GenerateJSONWebToken(res);
            IActionResult response = Ok(new LoginResponse { Token = tokenString, Email = _manager.Email, Role = "manager" });
            return response;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(ManagerLoginDTO _manager)
        {
            IActionResult response = Unauthorized("Invalid Credentials!");
            var manager = await AuthenticateUser(_manager);
            if (manager != null)
            {
                var tokenString = GenerateJSONWebToken(manager);
                response = Ok(new LoginResponse { Token = tokenString, Email = _manager.Email, Role = "manager" });
            }
            return response;
        }

        private string GenerateJSONWebToken(Manager managerInfo)
        {
            if (managerInfo is null)
            {
                throw new ArgumentNullException(nameof(managerInfo));
            }
            List<Claim> claims = new List<Claim>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            claims.Add(new Claim("Email", managerInfo.Email));
            claims.Add(new Claim("Role", "Manager"));
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            var check = token;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<Manager> AuthenticateUser(ManagerLoginDTO _manager)
        {
            Manager manager = await _uw.Manager.GetManager(_manager);
            if(manager is null)
            {
                return null;
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password: _manager.Password,
                 salt: manager.salt,
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 1000,
                 numBytesRequested: 256 / 8
            ));
            if(hashed == manager.Password) {
                return manager;
            }
            return null;
        }
    }
}

/* 
private readonly IManagerRepository<Manager> _managerRepository;   IManagerRepository<Manager> managerRepository
Manager manager = await _managerRepository.GetManager(_manager);

var res = await _managerRepository.AddManager(newManager);
              or
var res = await _uw.Manager.AddManager(newManager);
              or
var res = await _uw.Manager.CreateAsync(newManager);
 */

/* 
 Under claims.Add(new Claim("Role", "Manager"));
 * In case u have role property in model
if (userInfo.IsEmployee)
{
    claims.Add(new Claim("role", "admin"));
}
else
{
    claims.Add(new Claim("role", "POC"));
}
*/