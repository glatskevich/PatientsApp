using Microsoft.AspNetCore.Mvc;
using PatientsApp.BusinessLogic.Interfaces;
using PatientsApp.Common.Models;

namespace PatientsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(string id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientRequest patient)
        {
            patient = patient ?? throw new ArgumentNullException(nameof(patient));

            if (ModelState.IsValid)
            {
                await _patientService.CreatePatientAsync(patient);
                return Ok();
            }

            return BadRequest();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(string id, [FromBody] PatientRequest patient)
        {
            patient = patient ?? throw new ArgumentNullException(nameof(patient));

            if (ModelState.IsValid)
            {
                await _patientService.UpdatePatientAsync(id, patient);
                return NoContent();
            }

            return Conflict();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            await _patientService.DeletePatientAsync(id);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("search")]
        public async Task<IActionResult> SearchByBirthDate([FromQuery] string[] date)
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            string startPrefix = null;
            string endPrefix = null;

            if (date.Length == 0 || date.Length > 2)
            {
                return BadRequest("Допускается указание одной или двух дат");
            }

            if (date.Length >= 1)
            {
                startPrefix = date[0].Substring(0, 2);

                if (DateTime.TryParse(date[0].Substring(2), out var parsedStartDate) == false)
                {
                    return BadRequest("Неверный формат начальной даты");
                }

                startDate = parsedStartDate;
            }

            if (date.Length == 2)
            {
                endPrefix = date[1].Substring(0, 2);

                if (DateTime.TryParse(date[1].Substring(2), out var parsedEndDate) == false)
                {
                    return BadRequest("Неверный формат конечной даты");
                }

                endDate = parsedEndDate;
            }

            var patients = await _patientService.SearchPatientsByBirthDateAsync(startPrefix, startDate, endPrefix, endDate);
            return Ok(patients);
        }
    }
}
