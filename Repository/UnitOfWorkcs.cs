using PharmacyMedicineSupply.Repository.Classes;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PharmacySupplyContext _db;
        public UnitOfWork(PharmacySupplyContext db) {
            _db = db;
            Manager = new ManagerRepository(_db);
        }

        public IManagerRepository Manager { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
