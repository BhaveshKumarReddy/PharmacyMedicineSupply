using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySupplyProject.Models
{
    public class PharmacyMedicineSupply
    {
        public string PharmacyName { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public int SupplyCount { get; set; }

        public virtual Pharmacy Pharmacy { get; set; }
        public virtual MedicineDemand Medicine { get; set; }

    }
}
