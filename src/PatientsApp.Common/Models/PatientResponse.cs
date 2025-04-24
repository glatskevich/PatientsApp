namespace PatientsApp.Common.Models
{
    public class PatientResponse
    {
        public NameResponse Name { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Active { get; set; }
    }
}
