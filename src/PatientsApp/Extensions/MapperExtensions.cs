using PatientsApp.BusinessLogic.Mapping;

namespace PatientsApp.Extensions
{
    public static class MapperExtensions
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PatientProfile));
        }
    }
}
