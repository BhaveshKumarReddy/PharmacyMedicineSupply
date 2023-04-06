namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IMedicineStockRepository<MedicineStock>
    {
        Task<string> GetMedicineForSchedule(string ailment);

        Task<MedicineStock> GetStockByMedicineName(string medicinename);
        Task UpdateMedicineStock(MedicineStock medicineStock);
        public Task<IEnumerable<string>> GetMedicineStocksName();
    }
}
