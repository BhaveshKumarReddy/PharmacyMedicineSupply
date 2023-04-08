using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Models.DTO.ManagerDTO;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Task<Manager> AddManager(Manager manager);

        Task<Manager> GetManager(ManagerLoginDTO manager);
        Task<Manager> GetManagerbyname(string name);
        Task<Manager> GetManagerbymail(string email);
    }
}

// removed <> 
//