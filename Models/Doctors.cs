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
              Name = "Venkatesh",
              ContactNumber = "8532280017",
              TreatingAilment = "Cardiology"
          },
          new Doctor()
          {
              Name = "Akash",
              ContactNumber = "9232289112",
              TreatingAilment = "Orthopedic"
          },
          new Doctor()
          {
              Name = "Prakash",
              ContactNumber = "7569172719",
              TreatingAilment = "General"
          },
          new Doctor()
          {
              Name = "Suresh",
              ContactNumber = "7169100701",
              TreatingAilment = "Orthopedic"
          },
          new Doctor()
          {
              Name = "Ramanujan",
              ContactNumber = "8187002119",
              TreatingAilment = "ENT"
          }
        };

        public List<Doctor> getDoc()
        {
            return doctors;
        }

        public int getLength()
        {
            return doctors.Count;
        }
    }
}
