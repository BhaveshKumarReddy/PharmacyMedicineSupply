using AutoMapper;
using PharmacyMedicineSupply.Models.DTO.MedicineSupply;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<MedicineStock, MedicineStockDTO>().ReverseMap();



        }
    }
}
