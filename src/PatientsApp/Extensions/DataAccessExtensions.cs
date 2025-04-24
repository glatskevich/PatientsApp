using Microsoft.EntityFrameworkCore;
using PatientsApp.DataAccess.Contexts;
using PatientsApp.DataAccess.Interfaces;
using PatientsApp.DataAccess.Repositories;

namespace PatientsApp.Extensions
{
    public static class DataAccessExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PatientContext>(
                options => options.UseSqlServer(connectionString));

            services.AddScoped<IPatientRepository, PatientRepository>();
        }
    }
}
