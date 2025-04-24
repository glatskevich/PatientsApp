using AutoMapper;
using PatientsApp.BusinessLogic.Interfaces;
using PatientsApp.Common.Models;
using PatientsApp.DataAccess.Entities;
using PatientsApp.DataAccess.Interfaces;

namespace PatientsApp.BusinessLogic.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientResponse>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetPatientsAsync();
            return _mapper.Map<IEnumerable<PatientResponse>>(patients);
        }

        public async Task<PatientResponse> GetPatientByIdAsync(string patientId)
        {
            var patient = await _patientRepository.GetPatientAsync(patientId);

            if (patient is null)
            {
                throw new KeyNotFoundException($"Пациент с id {patientId} не найден");
            }

            return _mapper.Map<PatientResponse>(patient);
        }

        public async Task CreatePatientAsync(PatientRequest model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            var patient = _mapper.Map<Patient>(model);
            await _patientRepository.CreateAsync(patient);
        }

        public async Task UpdatePatientAsync(string patientId, PatientRequest model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            var entity = await _patientRepository.GetPatientAsync(patientId);

            if (entity is null)
            {
                throw new KeyNotFoundException($"Пациент с id {patientId} не найден");
            }

            entity.FamilyName = model.FamilyName;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Use = model.Use;
            entity.GenderId = model.GenderId;
            entity.BirthDate = model.BirthDate;
            entity.IsActive = model.IsActive;

            await _patientRepository.UpdateAsync(entity);
        }

        public async Task DeletePatientAsync(string patientId)
        {
            var entity = await _patientRepository.GetPatientAsync(patientId);

            if (entity is null)
            {
                throw new KeyNotFoundException($"Пациент с id {patientId} не найден");
            }

            await _patientRepository.DeletePatientAsync(patientId);
        }

        public async Task<IEnumerable<PatientResponse>> SearchPatientsByBirthDateAsync(string startPrefix, DateTime startDate, string endPrefix, DateTime endDate)
        {
            var patients = await _patientRepository.SearchByBirthDateAsync(startPrefix, startDate, endPrefix, endDate);
            return _mapper.Map<IEnumerable<PatientResponse>>(patients);
        }
    }
}
