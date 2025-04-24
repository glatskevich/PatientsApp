namespace PatientsApp.Common.Models
{
    public class PatientRequest
    {
        public string Use { get; set; }

        public string FamilyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GenderId { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; }
    }
}
