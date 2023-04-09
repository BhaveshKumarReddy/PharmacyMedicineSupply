using PharmacyMedicineSupply.Models.DTO.MedicineStock;
using PharmacyMedicineSupply.Models.DTO.MedicineSupply;

namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IMedicineStockRepository<MedicineStock>
    {
        Task<string> GetMedicineForSchedule(string ailment);

        Task<MedicineStock> GetStockByMedicineName(string medicinename);
        Task UpdateMedicineStock(MedicineStock medicineStock);
        Task<IEnumerable<string>> GetMedicineStocksName();
        Task<MedicineStockResponse> GetMedicineStocks(int page);
    }
}
