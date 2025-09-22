using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using TaskManagement.Backend.SeedData;

namespace TaskManagement.Backend.SeedData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("🌱 Property Management Data Seeder");
            Console.WriteLine("==================================");

            try
            {
                // MongoDB connection string
                var connectionString = "mongodb://localhost:27017";
                var databaseName = "PropertyManagement";

                // Create MongoDB client
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);

                // Create seeder
                var seeder = new DataSeeder(database);

                // Run seeding
                await seeder.SeedDataAsync();

                Console.WriteLine("\n✅ Data seeding completed successfully!");
                Console.WriteLine("You can now start the API and see the populated data.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error during seeding: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("\nSeeding process completed.");
        }
    }
}
