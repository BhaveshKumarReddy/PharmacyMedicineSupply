using Microsoft.EntityFrameworkCore;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PharmacySupplyContext _db;

        internal DbSet<T> dbSet;
        public Repository(PharmacySupplyContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task<T> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
            return entity;
        }

        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
