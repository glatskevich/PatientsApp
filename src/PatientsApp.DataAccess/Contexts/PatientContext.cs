using Microsoft.EntityFrameworkCore;
using PatientsApp.DataAccess.Configurations;
using PatientsApp.DataAccess.Entities;
using PatientsApp.DataAccess.Extensions;

namespace PatientsApp.DataAccess.Contexts
{
    public class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options) : base(options) 
        { }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());

            modelBuilder.InitializeData();
            base.OnModelCreating(modelBuilder);
        }
    }
}
