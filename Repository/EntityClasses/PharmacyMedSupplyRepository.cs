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
        public async Task AddPharmacyMedSupply(PharmacyMedSupply pharmacyMedicineSupply)
        {
            await _db.PharmacyMedicineSupplies.AddAsync(pharmacyMedicineSupply);
            await _db.SaveChangesAsync();
 
        }

        public async Task<IEnumerable<PharmacyMedSupplyDTO>> GetPharmacyMedicineSupply()
        {
            return await _db.PharmacyMedicineSupplies.Select(x => _mapper.Map<PharmacyMedSupplyDTO>(x)).ToListAsync();
        }

        public async Task<IEnumerable<PharmacyMedSupplyDTO>> GetPharmacyMedicineSupplyByDate(DateTime startDate)
        {
            var list = await _db.PharmacyMedicineSupplies.Where(x => x.DateTime == startDate).ToListAsync();
            return list.Select(x => _mapper.Map<PharmacyMedSupplyDTO>(x));
        }
    }
}
