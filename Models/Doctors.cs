namespace PharmacySupplyProject.Models
{
    public class Doctors
    {
        public Doctors() { }
        List<Doctor> doctors = new List<Doctor>() {
          new Doctor()
          {
              Name = "Aran",
              ContactNumber = "9542289337",
              TreatingAilment = "General"
          },
          new Doctor()
          {
              Name = "Avinash",
              ContactNumber = "8532280017",
              TreatingAilment = "Cardio"
          },
          new Doctor()
          {
              Name = "Adarsh",
              ContactNumber = "9232289112",
              TreatingAilment = "Ortho"
          },
          new Doctor()
          {
              Name = "Pavith",
              ContactNumber = "7569172719",
              TreatingAilment = "General"
          },
          new Doctor()
          {
              Name = "Parthe",
              ContactNumber = "7169100701",
              TreatingAilment = "Ortho"
          }
        };

        public List<Doctor> getDoc()
        {
            return doctors;
        }
    }
}
