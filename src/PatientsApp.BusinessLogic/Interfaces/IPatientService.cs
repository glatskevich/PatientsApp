using PatientsApp.Common.Models;

namespace PatientsApp.BusinessLogic.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientResponse>> GetAllPatientsAsync();

        Task<PatientResponse> GetPatientByIdAsync(string patientId);

        Task CreatePatientAsync(PatientRequest patient);

        Task UpdatePatientAsync(string patientId, PatientRequest patient);

        Task DeletePatientAsync(string patientId);

        Task<IEnumerable<PatientResponse>> SearchPatientsByBirthDateAsync(string startPrefix, DateTime startDate, string endPrefix, DateTime endDate);
    }
}
