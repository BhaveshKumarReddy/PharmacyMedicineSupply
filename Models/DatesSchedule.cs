using System.ComponentModel.DataAnnotations;

namespace PharmacyMedicineSupply.Models
{
    public class DatesSchedule
    {
        [Key]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte Completed { get; set; }
        public byte Supplied { get; set; }
    }
}
