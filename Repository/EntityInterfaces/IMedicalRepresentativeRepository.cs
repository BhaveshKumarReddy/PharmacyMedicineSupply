namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IMedicalRepresentativeRepository<MedicalRepresentativeDTO>
    {
        Task<List<MedicalRepresentativeDTO>> GetMedicalRepresentatives();
    }
}
