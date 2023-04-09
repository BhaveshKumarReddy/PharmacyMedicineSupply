using AutoMapper;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Models.DTO.MedicalRepresentative;
using PharmacyMedicineSupply.Repository.Classes;
using PharmacyMedicineSupply.Repository.EntityClasses;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PharmacySupplyContext _db;
        private readonly IMapper _mapper;
        public UnitOfWork(PharmacySupplyContext db, IMapper mapper) {
            _db = db;
            _mapper = mapper;
            ManagerRepository = new ManagerRepository(_db);
            PharmacyRepository = new PharmacyRepository(_db);
            MedicineDemandRepository = new MedicineDemandRepository(_db);
            DatesScheduleRepository = new DatesScheduleRepository(_db);
            MedicineStockRepository = new MedicineStockRepository(_db, _mapper);
            PharmacyMedSupplyRepository = new PharmacyMedSupplyRepository(_db, _mapper);
            MedicalRepresentativeRepository = new MedicalRepresentativeRepository(_db, _mapper);
            RepresentativeScheduleRepository = new RepresentativeScheduleRepository(_db, DatesScheduleRepository);
        }

        public IManagerRepository ManagerRepository { get; private set; }
        public IPharmacyRepository<Pharmacy> PharmacyRepository { get; private set; }
        public IDatesScheduleRepository<DatesSchedule> DatesScheduleRepository { get; private set; }
        public IMedicineStockRepository<MedicineStock> MedicineStockRepository { get; private set; }
        public IMedicineDemandRepository<MedicineDemand> MedicineDemandRepository { get; private set; }
        public IPharmacyMedSupplyRepository<PharmacyMedSupply> PharmacyMedSupplyRepository { get; private set; }
        public IMedicalRepresentativeRepository<MedicalRepresentativeDTO> MedicalRepresentativeRepository { get; private set; }
        public IRepresentativeScheduleRepository<RepresentativeSchedule> RepresentativeScheduleRepository { get; private set; }

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
