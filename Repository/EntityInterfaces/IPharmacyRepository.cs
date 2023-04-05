namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IPharmacyRepository<Pharmacy>
    {
        List<Pharmacy> GetAllPharmacies();   
    }
}
