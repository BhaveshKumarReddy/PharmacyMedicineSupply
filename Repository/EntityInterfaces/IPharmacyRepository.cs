namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IPharmacyRepository<Pharmacy>
    {
        Task<IEnumerable<Pharmacy>> GetAllPharmacies();   
    }
}
