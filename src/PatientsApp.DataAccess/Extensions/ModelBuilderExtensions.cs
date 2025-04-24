using Microsoft.EntityFrameworkCore;
using PatientsApp.DataAccess.Entities;

namespace PatientsApp.DataAccess.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static ModelBuilder InitializeData(this ModelBuilder modelBuilder)
        {
            modelBuilder.InitializeGenders();
            return modelBuilder;
        }

        private static ModelBuilder InitializeGenders(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(
                new Gender
                {
                    Id = 1,
                    Name = "male"
                },
                new Gender
                {
                    Id = 2,
                    Name = "female",
                },
                new Gender
                {
                    Id = 3,
                    Name = "other",
                },
                new Gender
                {
                    Id = 4,
                    Name = "unknown"
                }
            );

            return modelBuilder;
        }
    }
}
