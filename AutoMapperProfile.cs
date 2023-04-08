using AutoMapper;
using PharmacyMedicineSupply.Models.DTO.MedicalRepresentative;
using PharmacyMedicineSupply.Models.DTO.MedicineSupply;
using PharmacyMedicineSupply.Models.DTO.PharmacyMedSupply;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MedicineStock, MedicineStockDTO>().ReverseMap();
            CreateMap<PharmacyMedSupply, PharmacyMedSupplyDTO>().ReverseMap();
            CreateMap<MedicalRepresentative, MedicalRepresentativeDTO>().ReverseMap();
        }
    }
}
