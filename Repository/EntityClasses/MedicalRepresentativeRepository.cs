using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models.DTO.MedicalRepresentative;
using PharmacyMedicineSupply.Models.DTO.MedicineSupply;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Repository.EntityClasses
{
    public class MedicalRepresentativeRepository : IMedicalRepresentativeRepository<MedicalRepresentativeDTO>
    {
        private readonly PharmacySupplyContext _db;
        private readonly IMapper _mapper;
        public MedicalRepresentativeRepository(PharmacySupplyContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<MedicalRepresentativeDTO>> GetMedicalRepresentatives()
        {
            var reps = await _db.MedicalRepresentatives.ToListAsync();
            return reps.Select(x => _mapper.Map<MedicalRepresentativeDTO>(x)).ToList();
        }
    }
}
