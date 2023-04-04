using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;
using System.Collections.Generic;

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

        private readonly IMedicineStockReposiroty<MedicineStock> _medicineRepo;

        public MedicalRepresentativeScheduleController(IMedicineStockReposiroty<MedicineStock> medicineRepo) {
            _medicineRepo = medicineRepo;
        }

        [HttpGet]
        public async Task<List<RepresentativeSchedule>> GetRepSchedule()
        {
            DateTime getCur = DateTime.Today;
            DateTime cur = DateTime.Today;

            Dictionary<Doctor,string> dict = MapRepsDoctors();

            List<RepresentativeSchedule> representativeSchedules = new();

            Dictionary<string, string> ailment_dict = new();

            foreach (KeyValuePair<Doctor, string> data in dict)
            {
                string medicines = "";
                string treating_ailment = data.Key.TreatingAilment;
                if (ailment_dict.ContainsKey(treating_ailment))
                {
                    medicines = ailment_dict[treating_ailment];
                }
                else
                {
                    medicines = await _medicineRepo.GetMedicineForSchedule(data.Key.TreatingAilment);
                    ailment_dict.Add(treating_ailment, medicines);
                }

                RepresentativeSchedule schedule = new();
                schedule.DoctorName = data.Key.Name;
                schedule.DoctorContactNumber = data.Key.ContactNumber;
                schedule.TreatingAilment = data.Key.TreatingAilment;
                schedule.RepresentativeName = data.Value;
                schedule.Slot = DateTime.Now;
                schedule.Medicine = medicines;
                schedule.Date = cur;

                representativeSchedules.Add(schedule);

                cur = cur.AddDays(1);

                if (cur.Equals(getCur.AddDays(5)))
                {
                    cur = getCur;
                }

            }

            return representativeSchedules;
        }

        private Dictionary<Doctor,string> MapRepsDoctors()
        {
            int days = 5;

            int doctors_count = doctors.Count;

            int reps_count = reps.Count;

            int count = 0;

            int x, y = 0;

            Dictionary<Doctor,string> map = new();

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
                map.Add(doctors[count - 1],reps[x - 1]);
            }

            return map;
        }
    }
}
