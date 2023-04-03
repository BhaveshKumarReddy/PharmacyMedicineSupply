using System.ComponentModel.DataAnnotations;

namespace PharmacySupplyProject.Models
{
    public class RepresentativeSchedule
    {
        public string RepresentativeName { get; set; } = string.Empty;
        public string DoctorName { get; set; } =    string.Empty;
        public string TreatingAilment { get; set; } = string.Empty;
        public string Medicine { get; set; } = string.Empty;

        [DataType(DataType.Time)]
        public DateTime Slot { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [StringLength(10)]
        public string DoctorContactNumber { get; set; } = string.Empty;
    }
}
