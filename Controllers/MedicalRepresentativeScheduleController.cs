using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Models.DTO.MedicalRepresentative;
using PharmacyMedicineSupply.Repository;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;
using System.Collections.Generic;

namespace PharmacyMedicineSupply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRepresentativeScheduleController : ControllerBase
    {

        private readonly IUnitOfWork _uw;

        public MedicalRepresentativeScheduleController(IUnitOfWork uw) {
            _uw = uw;
        }

        public List<Doctor> doctors = new Doctors().getDoc();


        [HttpGet("GetScheduleByDate/{startDateString}")]
        public async Task<ActionResult<IEnumerable<RepresentativeSchedule>>> GetScheduleByDate(string startDateString)
        {
            DateTime startDate = Convert.ToDateTime(startDateString);
            try
            {
                return await _uw.RepresentativeScheduleRepository.GetScheduleByDate(startDate);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CreateSchedule/{startDateString}")]
        public async Task<ActionResult<IEnumerable<RepresentativeSchedule>>> CreateSchedule(string startDateString)
        {
            try
            {
                DateTime startDate = Convert.ToDateTime(startDateString);
                DateTime newDate = startDate;
                DateTime endDate = startDate;

                Dictionary<Doctor, string> map_dict = await MapRepsDoctors();
                Dictionary<string, string> ailment_dict = new();

                List<RepresentativeSchedule> representativeSchedules = new();

                int total_days = 5, slot_time = 1;

                foreach (KeyValuePair<Doctor, string> data in map_dict)
                {
                    string medicines = "";
                    string treating_ailment = data.Key.TreatingAilment;
                    if (ailment_dict.ContainsKey(treating_ailment))
                    {
                        medicines = ailment_dict[treating_ailment];
                    }
                    else
                    {
                        medicines = await _uw.MedicineStockRepository.GetMedicineForSchedule(data.Key.TreatingAilment);
                        ailment_dict.Add(treating_ailment, medicines);
                    }

                    if (newDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        newDate = newDate.AddDays(1);
                        total_days += 1;
                    }

                    RepresentativeSchedule schedule = new();
                    schedule.DoctorName = data.Key.Name;
                    schedule.DoctorContactNumber = data.Key.ContactNumber;
                    schedule.TreatingAilment = data.Key.TreatingAilment;
                    schedule.RepresentativeName = data.Value;
                    schedule.Slot = slot_time + "pm - " + (slot_time + 1) + "pm";
                    schedule.Medicine = medicines;
                    schedule.Date = newDate;

                    representativeSchedules.Add(schedule);

                    endDate = (newDate>endDate)?newDate:endDate;
                    newDate = newDate.AddDays(1);

                    if (newDate.Equals(startDate.AddDays(total_days)))
                    {
                        newDate = startDate;
                        total_days = 5;
                    }

                    if (newDate.Equals(startDate))
                    {
                        slot_time += 1;
                    }
                }

                DatesSchedule period = new();
                period.StartDate = startDate;
                period.EndDate = endDate;

                await _uw.DatesScheduleRepository.AddDateSchedule(period);

                await _uw.RepresentativeScheduleRepository.AddSchedules(representativeSchedules);

                return representativeSchedules;
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<Dictionary<Doctor,string>> MapRepsDoctors()
        {
            List<MedicalRepresentativeDTO> reps = await _uw.MedicalRepresentativeRepository.GetMedicalRepresentatives();

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
                map.Add(doctors[count - 1],reps[x - 1].Name);
            }

            return map;
        }
    }
}
