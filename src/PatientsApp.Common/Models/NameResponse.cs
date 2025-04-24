namespace PatientsApp.Common.Models
{
    public class NameResponse
    {
        public string Id { get; set; }

        public string Use { get; set; }

        public string Family { get; set; }

        public List<string> Given { get; set; }
    }
}
