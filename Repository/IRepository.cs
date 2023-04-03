namespace PharmacyMedicineSupply.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T entity);
        Task SaveAsync();
    }
}
