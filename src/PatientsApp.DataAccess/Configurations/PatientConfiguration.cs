using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientsApp.DataAccess.Entities;

namespace PatientsApp.DataAccess.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Use)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(p => p.FamilyName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(p => p.GenderId)
                .IsRequired();

            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.HasOne(p => p.Gender)
                .WithMany(c => c.Patients)
                .HasForeignKey(p => p.GenderId);
        }
    }
}
