using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IMedicineDemandRepository<MedicineDemand>
    {
        Task<IEnumerable<MedicineDemand>> GetMedicineDemand();
        Task<MedicineDemand> AddMedicineDemand(MedicineDemand MedicineDemand);
        Task<MedicineDemand> UpdateMedicineDemand(String MedicineName);
        void DeleteMedicineDemand(String MedicineName);
            
      
    }
}
