using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Models.DTO.MedicalRepresentative;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository
{
    public interface IUnitOfWork
    {
        IManagerRepository ManagerRepository { get; }
        IPharmacyRepository<Pharmacy> PharmacyRepository { get; }
        IMedicineDemandRepository<MedicineDemand> MedicineDemandRepository { get; }
        IDatesScheduleRepository<DatesSchedule> DatesScheduleRepository { get; }
        IMedicineStockRepository<MedicineStock> MedicineStockRepository { get; }
        IPharmacyMedSupplyRepository<PharmacyMedSupply> PharmacyMedSupplyRepository { get; }
        IMedicalRepresentativeRepository<MedicalRepresentativeDTO> MedicalRepresentativeRepository { get; }
        IRepresentativeScheduleRepository<RepresentativeSchedule> RepresentativeScheduleRepository { get; }
        void Dispose();
        void Save();
    }
}
