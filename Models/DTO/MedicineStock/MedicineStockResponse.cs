using PharmacyMedicineSupply.Models.DTO.MedicineSupply;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Models.DTO.MedicineStock
{
    public class MedicineStockResponse
    {
        public List<MedicineStockDTO> MedicineStocks { get; set; } = new List<MedicineStockDTO>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
