﻿using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IMedicineDemandRepository<MedicineDemand>
    {
        List<MedicineDemand> GetMedicineDemand();
        Task<MedicineDemand> AddMedicineDemand(MedicineDemand MedicineDemand);
        Task<MedicineDemand> UpdateMedicineDemand(String MedicineName, int count);
        void DeleteMedicineDemand(String MedicineName);
        void ResetMedicineDemand();

            
      
    }
}
