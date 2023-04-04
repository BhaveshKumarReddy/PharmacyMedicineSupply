using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository
{
    public interface IUnitOfWork
    {
        IManagerRepository Manager { get; }
        void Dispose();
        void Save();
    }
}
