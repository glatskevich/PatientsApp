namespace PatientsApp.DataAccess.Entities
{
    public class Patient
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Use { get; set; }

        public string FamilyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GenderId { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; }
    }
}
