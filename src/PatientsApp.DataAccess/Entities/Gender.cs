﻿namespace PatientsApp.DataAccess.Entities
{
    public class Gender
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Patient> Patients { get; set; }
    }
}
