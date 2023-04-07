using System.ComponentModel.DataAnnotations;

namespace PharmacySupplyProject.Models
{
    public class Manager
    {
        [Key]
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public byte[]? salt { get; set; }
    }
}
