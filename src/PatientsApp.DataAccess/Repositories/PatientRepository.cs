using Microsoft.EntityFrameworkCore;
using PatientsApp.DataAccess.Contexts;
using PatientsApp.DataAccess.Entities;
using PatientsApp.DataAccess.Interfaces;

namespace PatientsApp.DataAccess.Repositories
{
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(PatientContext context) : base(context) { }

        public async Task<Patient> GetPatientAsync(string patientId)
        {
            return await _context.Patients
                .Include(p => p.Gender)
                .FirstOrDefaultAsync(p => p.Id == patientId);
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.Gender)
                .ToListAsync();
        }

        public async Task DeletePatientAsync(string patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Patient>> SearchByBirthDateAsync(string startPrefix, DateTime startDate, string endPrefix, DateTime endDate)
        {
            IQueryable<Patient> query = _context.Patients;

            if (startPrefix != null)
            {
                switch (startPrefix)
                {
                    case "eq":
                        query = query.Where(p => p.BirthDate.Date == startDate.Date);
                        break;
                    case "ne":
                        query = query.Where(p => p.BirthDate.Date != startDate.Date);
                        break;
                    case "gt":
                        query = query.Where(p => p.BirthDate > startDate);
                        break;
                    case "lt":
                        query = query.Where(p => p.BirthDate < startDate);
                        break;
                    case "ge":
                        query = query.Where(p => p.BirthDate >= startDate.Date);
                        break;
                    case "le":
                        query = query.Where(p => p.BirthDate.Date <= startDate);
                        break;
                    case "sa":
                        var nextDay = startDate.AddDays(1).Date;
                        query = query.Where(p => p.BirthDate > nextDay);
                        break;
                    case "eb":
                        query = query.Where(p => p.BirthDate < startDate.Date);
                        break;
                    case "ap":
                        var now = DateTime.Now;
                        var difference = startDate.Subtract(now).TotalDays;
                        var tolerance = Math.Abs(difference) * 0.1;

                        var approxStartDate = startDate.AddDays(-tolerance);
                        var approxEndDate = startDate.AddDays(tolerance);
                        query = query.Where(p => p.BirthDate >= approxStartDate && p.BirthDate <= approxEndDate);
                        break;

                    default:
                        throw new ArgumentException("Неверный формат начальной даты");
                }
            }

            if (endPrefix != null)
            {
                switch (endPrefix)
                {
                    case "eq":
                        query = query.Where(p => p.BirthDate.Date == endDate.Date);
                        break;
                    case "ne":
                        query = query.Where(p => p.BirthDate.Date != endDate.Date);
                        break;
                    case "gt":
                        query = query.Where(p => p.BirthDate > endDate);
                        break;
                    case "lt":
                        query = query.Where(p => p.BirthDate < endDate);
                        break;
                    case "ge":
                        query = query.Where(p => p.BirthDate >= endDate.Date);
                        break;
                    case "le":
                        query = query.Where(p => p.BirthDate.Date <= endDate);
                        break;
                    case "sa":
                        var nextDay = endDate.AddDays(1).Date;
                        query = query.Where(p => p.BirthDate > nextDay);
                        break;
                    case "eb": 
                        query = query.Where(p => p.BirthDate < endDate.Date);
                        break;

                    default:
                        throw new ArgumentException("Неверный формат конечной даты");
                }
            }

            return await query.ToListAsync();
        }
    }
}
