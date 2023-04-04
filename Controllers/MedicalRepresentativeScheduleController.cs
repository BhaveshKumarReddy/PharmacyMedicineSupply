using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models.DTO.MedicineSupply;
using PharmacySupplyProject.Models;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRepresentativeScheduleController : ControllerBase
    {
        
        public List<Doctor> doctors = new Doctors().getDoc();
        private readonly PharmacySupplyContext _context;
        public MedicalRepresentativeScheduleController(PharmacySupplyContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepresentativeSchedule>>> GetRepSchedule(DateTime date)
        {
            List<string> mapped = MapRepsDoctors(date);
            
            return await _context.RepresentativeSchedules.ToListAsync(); ;
        }

        private List<string> MapRepsDoctors(DateTime date)
        {
            int days = 5;
            int doctors_count = doctors.Count;
            List<MedicalRepresentative> reps = _context.MedicalRepresentatives.ToList();
            //List<MedicineStock> meds = _context.MedicineStocks.ToList();
            int reps_count = reps.Count;
            int count = 0;
            int x, y = 0;
            List<string> map = new List<string>();
            Boolean f = false;
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
                RepresentativeSchedule representativeSchedule = new RepresentativeSchedule();
                representativeSchedule.RepresentativeName = reps[x - 1].Name;
                representativeSchedule.DoctorName = doctors[count - 1].Name;
                representativeSchedule.DoctorContactNumber = doctors[count - 1].ContactNumber;
                if(date.AddDays(y-1).DayOfWeek == DayOfWeek.Sunday)
                {                    
                    f = true;
                }
                if (f)
                {
                    y += 1;
                    representativeSchedule.Date = date.AddDays(y - 1);
                }
                else
                {
                    representativeSchedule.Date = date.AddDays(y - 1);
                }                
                representativeSchedule.Slot = date.AddHours(12);
                representativeSchedule.TreatingAilment = doctors[count - 1].TreatingAilment;
                var xy = _context.MedicineStocks.Where(x => x.TargetAilment == doctors[count - 1].TreatingAilment);
                foreach(var m in xy)
                {                  
                    representativeSchedule.Medicine += m.Name + ",";
                }
                representativeSchedule.Medicine = representativeSchedule.Medicine.Remove(representativeSchedule.Medicine.Length - 1, 1);
                representativeSchedule.Medicine += ".";
                _context.RepresentativeSchedules.Add(representativeSchedule);
                _context.SaveChanges();
                map.Add(reps[x - 1] + "," + doctors[count - 1].Name + "On Day " + y);
            }
            return map;
        }
    }
}
