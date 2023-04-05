using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models.DTO.MedicineSupply;
using PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class PharmacyMedSupplyRepository : IPharmacyMedSupplyRepository<PharmacyMedSupply>
    {
        private readonly PharmacySupplyContext _db;
        private readonly IMapper _mapper;
        public PharmacyMedSupplyRepository(PharmacySupplyContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public void AddPharmacyMedSupply(PharmacyMedSupply pharmacyMedicineSupply)
        {
            _db.PharmacyMedicineSupplies.Add(pharmacyMedicineSupply);
            _db.SaveChanges();
 
        }

        public async Task<IEnumerable<PharmacyMedSupplyDTO>> GetPharmacyMedicineSupply()
        {
            return await _db.PharmacyMedicineSupplies.Select(x => _mapper.Map<PharmacyMedSupplyDTO>(x)).ToListAsync();
        }
    }
}
