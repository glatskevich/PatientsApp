using PatientsApp.BusinessLogic.Interfaces;
using PatientsApp.BusinessLogic.Services;

namespace PatientsApp.Extensions
{
    public static class BusinessLogicExtensions
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
        }
    }
}
