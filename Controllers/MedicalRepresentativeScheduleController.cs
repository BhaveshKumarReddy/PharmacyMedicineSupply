using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRepresentativeScheduleController : ControllerBase
    {
        public List<String> reps = new List<String>() {
             "Roshan", "Neha", "Rebitha"
        };
        public List<Doctor> doctors = new Doctors().getDoc();

        public MedicalRepresentativeScheduleController() { }

        [HttpGet]
        public ActionResult GetRepSchedule()
        {
            List<string> mapped = MapRepsDoctors();
            return Ok();
        }

        public List<string> MapRepsDoctors()
        {
            int days = 5;

            int doctors_count = doctors.Count;

            int reps_count = reps.Count;

            int count = 0;

            int x, y = 0;

            List<string> map = new List<string>();

            while (doctors_count > 0)
            {
                count += 1;
                doctors_count -= 1;
                x = count % reps_count;
                if (x == 0)
                {
                    x = reps_count;
                }
                y = count % days;
                if (y == 0)
                {
                    y = days;
                }
                map.Add(reps[x - 1] + "," + doctors[count - 1].Name + "On Day " + y);
            }

            return map;
        }
    }
}
