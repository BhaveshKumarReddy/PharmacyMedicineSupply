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
              TreatingAilment = "Cardiology"
          },
          new Doctor()
          {
              Name = "Adarsh",
              ContactNumber = "9232289112",
              TreatingAilment = "Orthopedic"
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
              TreatingAilment = "Orthopedic"
          },
          new Doctor()
          {
              Name = "Prabhakar",
              ContactNumber = "8187002119",
              TreatingAilment = "ENT"
          },
          new Doctor()
          {
              Name = "Aran1",
              ContactNumber = "9542289337",
              TreatingAilment = "General"
          },
          new Doctor()
          {
              Name = "Avinash2",
              ContactNumber = "8532280017",
              TreatingAilment = "Cardiology"
          },
          new Doctor()
          {
              Name = "Adarsh3",
              ContactNumber = "9232289112",
              TreatingAilment = "Orthopedic"
          },
          new Doctor()
          {
              Name = "Pavith4",
              ContactNumber = "7569172719",
              TreatingAilment = "General"
          },
          new Doctor()
          {
              Name = "Parthe5",
              ContactNumber = "7169100701",
              TreatingAilment = "Orthopedic"
          },
          new Doctor()
          {
              Name = "Prabhakar6",
              ContactNumber = "8187002119",
              TreatingAilment = "ENT"
          }
        };

        public List<Doctor> getDoc()
        {
            return doctors;
        }
    }
}
