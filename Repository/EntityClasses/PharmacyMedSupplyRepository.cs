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

        public async Task<List<PharmacyMedSupplyDTO>> GetPharmacyMedicineSupply()
        {
            return await _db.PharmacyMedicineSupplies.Select(x => _mapper.Map<PharmacyMedSupplyDTO>(x)).ToListAsync();
        }

        public async Task<PharmacyMedSupplyResponse> GetPharmacyMedicineSupplyByDate(int page,DateTime startDate)
        {
            var pageResults = 6f;
            var pageCount = Math.Ceiling(_db.PharmacyMedicineSupplies.Where(x => x.DateTime == startDate).Count() / pageResults);
            var supplies = await _db.PharmacyMedicineSupplies
                                  .Where(x => x.DateTime == startDate)
                                  .Skip((page - 1) * (int)pageResults)
                                  .Take((int)pageResults)
                                  .Select(x => _mapper.Map<PharmacyMedSupplyDTO>(x)).ToListAsync();

            var response = new PharmacyMedSupplyResponse
            {
                pharmacyMedSupplies = supplies,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return response;
        }
    }
}
