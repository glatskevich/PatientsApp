using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace PatientLoader
{
    class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();

        static async Task Main()
        {
            httpClient.BaseAddress = new Uri("https://localhost:7264/api/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            for (int i = 1; i <= 100; i++)
            {
                var patient = CreatePatient(i);
                var json = JsonConvert.SerializeObject(patient);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("patient", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Пациент {i} создан успешно");
                }
                else
                {
                    Console.WriteLine($"Ошибка при создании пациента {i}: {response.ReasonPhrase}");
                }
            }

            Console.ReadKey();
        }

        private static Patient CreatePatient(int index)
        {
            var random = new Random();

            string[] firstNames = { "Иван", "Сергей", "Алексей", "Дмитрий", "Андрей" };
            string[] lastNames = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Семёнов" };
            string[] middleNames = { "Иванович", "Сергеевич", "Алексеевич", "Дмитриевич", "Андреевич" };

            string firstName = firstNames[random.Next(0, firstNames.Length)];
            string lastName = lastNames[random.Next(0, lastNames.Length)];
            string middleName = middleNames[random.Next(0, middleNames.Length)];

            return new Patient
            {
                Use = "official",
                FamilyName = lastName,
                FirstName = firstName,
                LastName = middleName,
                GenderId = 1,
                BirthDate = DateTime.Now.AddYears(-10).AddDays(index),
                IsActive = true
            };
        }
    }
}
