using PatientsApp.DataAccess.Entities;

namespace PatientsApp.DataAccess.Interfaces
{
    public interface IPatientRepository : IRepositoryBase<Patient>
    {
        Task<List<Patient>> GetPatientsAsync();

        Task<Patient> GetPatientAsync(string patientId);

        Task DeletePatientAsync(string patientId);

        Task<List<Patient>> SearchByBirthDateAsync(string startPrefix, DateTime startDate, string endPrefix, DateTime endDate);
    }
}
