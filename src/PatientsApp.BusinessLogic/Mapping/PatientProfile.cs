using AutoMapper;
using PatientsApp.Common.Models;
using PatientsApp.DataAccess.Entities;

namespace PatientsApp.BusinessLogic.Mapping
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientResponse>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Name))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameResponse
                {
                    Id = src.Id,
                    Use = src.Use,
                    Family = src.FamilyName,
                    Given = new List<string> { src.FirstName, src.LastName }
                }));

            CreateMap<PatientRequest, Patient>()
                .ForMember(dest => dest.Gender, opt => opt.Ignore());
        }
    }
}
