using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacySupplyProject.Models
{
    public class RepresentativeSchedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RepresentativeName { get; set; } = string.Empty;
        public string DoctorName { get; set; } =    string.Empty;
        public string TreatingAilment { get; set; } = string.Empty;
        public string Medicine { get; set; } = string.Empty;
        public string Slot { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [StringLength(10)]
        public string DoctorContactNumber { get; set; } = string.Empty;
        public byte Status { get; set; }
    }
}
