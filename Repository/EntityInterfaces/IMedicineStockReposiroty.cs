namespace PharmacyMedicineSupply.Repository.EntityInterfaces
{
    public interface IMedicineStockReposiroty<MedicineStock>
    {
        Task<string> GetMedicineForSchedule(string ailment);

        MedicineStock GetStockByMedicineName(string medicinename);
    }
}
