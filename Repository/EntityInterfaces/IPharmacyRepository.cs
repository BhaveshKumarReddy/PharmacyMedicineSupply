namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IPharmacyRepository<Pharmacy>
    {
        Task<List<Pharmacy>> GetAllPharmacies();   
    }
}
