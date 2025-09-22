using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Backend.SeedData
{
    public class DataSeeder
    {
        private readonly IMongoDatabase _database;

        public DataSeeder(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task SeedDataAsync()
        {
            Console.WriteLine("🌱 Starting data seeding...");

            // Clear existing data
            await ClearExistingData();

            // Create owners
            var owners = await CreateOwners();
            Console.WriteLine($"✅ Created {owners.Count} owners");

            // Create properties
            var properties = await CreateProperties(owners);
            Console.WriteLine($"✅ Created {properties.Count} properties");

            // Create property traces
            var traces = await CreatePropertyTraces(properties);
            Console.WriteLine($"✅ Created {traces.Count} property traces");

            Console.WriteLine("🎉 Data seeding completed successfully!");
        }

        private async Task ClearExistingData()
        {
            Console.WriteLine("🧹 Clearing existing data...");

            await _database.GetCollection<Owner>("Owners").DeleteManyAsync(_ => true);
            await _database.GetCollection<Property>("Properties").DeleteManyAsync(_ => true);
            await _database.GetCollection<PropertyTrace>("PropertyTraces").DeleteManyAsync(_ => true);
            await _database.GetCollection<PropertyImage>("PropertyImages").DeleteManyAsync(_ => true);
        }

        private async Task<List<Owner>> CreateOwners()
        {
            var owners = new List<Owner>
            {
                new Owner
                {
                    IdOwner = Guid.NewGuid().ToString(),
                    Name = "María González Rodríguez",
                    Address = "Carrera 15 #93-47, Bogotá, Cundinamarca",
                    Photo = "https://images.unsplash.com/photo-1494790108755-2616b612b786?w=150&h=150&fit=crop&crop=face",
                    Birthday = new DateTime(1985, 3, 15),
                    CreatedAt = DateTime.UtcNow.AddDays(-45),
                    UpdatedAt = null
                },
                new Owner
                {
                    IdOwner = Guid.NewGuid().ToString(),
                    Name = "Carlos Alberto Pérez",
                    Address = "Calle 127 #15-32, Bogotá, Cundinamarca",
                    Photo = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=150&h=150&fit=crop&crop=face",
                    Birthday = new DateTime(1978, 7, 22),
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    UpdatedAt = null
                },
                new Owner
                {
                    IdOwner = Guid.NewGuid().ToString(),
                    Name = "Ana Lucía Martínez",
                    Address = "Avenida 68 #25-41, Medellín, Antioquia",
                    Photo = "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=150&h=150&fit=crop&crop=face",
                    Birthday = new DateTime(1990, 11, 8),
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    UpdatedAt = null
                },
                new Owner
                {
                    IdOwner = Guid.NewGuid().ToString(),
                    Name = "Roberto Silva Herrera",
                    Address = "Carrera 50 #26-20, Cali, Valle del Cauca",
                    Photo = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=150&h=150&fit=crop&crop=face",
                    Birthday = new DateTime(1982, 1, 30),
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    UpdatedAt = null
                },
                new Owner
                {
                    IdOwner = Guid.NewGuid().ToString(),
                    Name = "Isabel Cristina López",
                    Address = "Calle 100 #11-15, Bogotá, Cundinamarca",
                    Photo = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=150&h=150&fit=crop&crop=face",
                    Birthday = new DateTime(1987, 5, 12),
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    UpdatedAt = null
                },
                new Owner
                {
                    IdOwner = Guid.NewGuid().ToString(),
                    Name = "Diego Fernando Ramírez",
                    Address = "Carrera 7 #32-16, Bogotá, Cundinamarca",
                    Photo = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=150&h=150&fit=crop&crop=face",
                    Birthday = new DateTime(1992, 9, 3),
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    UpdatedAt = null
                },
                new Owner
                {
                    IdOwner = Guid.NewGuid().ToString(),
                    Name = "Patricia Morales Vega",
                    Address = "Avenida 19 #104-07, Bogotá, Cundinamarca",
                    Photo = "https://images.unsplash.com/photo-1487412720507-e7ab37603c6f?w=150&h=150&fit=crop&crop=face",
                    Birthday = new DateTime(1980, 12, 18),
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    UpdatedAt = null
                },
                new Owner
                {
                    IdOwner = Guid.NewGuid().ToString(),
                    Name = "Andrés Felipe Castro",
                    Address = "Calle 85 #11-30, Bogotá, Cundinamarca",
                    Photo = "https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?w=150&h=150&fit=crop&crop=face",
                    Birthday = new DateTime(1988, 4, 25),
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    UpdatedAt = null
                }
            };

            var collection = _database.GetCollection<Owner>("Owners");
            await collection.InsertManyAsync(owners);
            return owners;
        }

        private async Task<List<Property>> CreateProperties(List<Owner> owners)
        {
            var properties = new List<Property>
            {
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Apartamento Zona Rosa",
                    Address = "Carrera 15 #93-47, Zona Rosa, Bogotá",
                    Price = 850000000,
                    CodeInternal = "APT-001-2024",
                    Year = 2020,
                    IdOwner = owners[0].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddDays(-40),
                    UpdatedAt = null
                },
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Casa Familiar Chapinero",
                    Address = "Calle 127 #15-32, Chapinero, Bogotá",
                    Price = 1200000000,
                    CodeInternal = "CASA-002-2024",
                    Year = 2018,
                    IdOwner = owners[1].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddDays(-35),
                    UpdatedAt = null
                },
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Penthouse El Poblado",
                    Address = "Avenida 68 #25-41, El Poblado, Medellín",
                    Price = 1500000000,
                    CodeInternal = "PENT-003-2024",
                    Year = 2021,
                    IdOwner = owners[2].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddDays(-25),
                    UpdatedAt = null
                },
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Oficina Centro Comercial",
                    Address = "Carrera 50 #26-20, Centro, Cali",
                    Price = 450000000,
                    CodeInternal = "OFI-004-2024",
                    Year = 2019,
                    IdOwner = owners[3].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    UpdatedAt = null
                },
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Apartamento Usaquén",
                    Address = "Calle 100 #11-15, Usaquén, Bogotá",
                    Price = 680000000,
                    CodeInternal = "APT-005-2024",
                    Year = 2022,
                    IdOwner = owners[4].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    UpdatedAt = null
                },
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Casa Campestre La Calera",
                    Address = "Carrera 7 #32-16, La Calera, Cundinamarca",
                    Price = 2200000000,
                    CodeInternal = "CASA-006-2024",
                    Year = 2017,
                    IdOwner = owners[5].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddDays(-12),
                    UpdatedAt = null
                },
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Apartamento Teusaquillo",
                    Address = "Avenida 19 #104-07, Teusaquillo, Bogotá",
                    Price = 720000000,
                    CodeInternal = "APT-007-2024",
                    Year = 2020,
                    IdOwner = owners[6].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddDays(-8),
                    UpdatedAt = null
                },
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Local Comercial Zona T",
                    Address = "Calle 85 #11-30, Zona T, Bogotá",
                    Price = 320000000,
                    CodeInternal = "LOC-008-2024",
                    Year = 2023,
                    IdOwner = owners[7].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    UpdatedAt = null
                },
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Apartamento Rosales",
                    Address = "Carrera 11 #84-25, Rosales, Bogotá",
                    Price = 950000000,
                    CodeInternal = "APT-009-2024",
                    Year = 2021,
                    IdOwner = owners[0].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    UpdatedAt = null
                },
                new Property
                {
                    IdProperty = Guid.NewGuid().ToString(),
                    Name = "Casa Barrio La Candelaria",
                    Address = "Calle 10 #3-15, La Candelaria, Bogotá",
                    Price = 1800000000,
                    CodeInternal = "CASA-010-2024",
                    Year = 2015,
                    IdOwner = owners[1].IdOwner,
                    CreatedAt = DateTime.UtcNow.AddHours(-12),
                    UpdatedAt = null
                }
            };

            var collection = _database.GetCollection<Property>("Properties");
            await collection.InsertManyAsync(properties);

            // Create property images
            await CreatePropertyImages(properties);

            return properties;
        }

        private async Task CreatePropertyImages(List<Property> properties)
        {
            var images = new List<PropertyImage>();
            var imageUrls = new[]
            {
                "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=800&h=600&fit=crop",
                "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=800&h=600&fit=crop",
                "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800&h=600&fit=crop",
                "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800&h=600&fit=crop",
                "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800&h=600&fit=crop",
                "https://images.unsplash.com/photo-1600607687644-c7171b42498b?w=800&h=600&fit=crop",
                "https://images.unsplash.com/photo-1600566753190-17f0baa2a6c3?w=800&h=600&fit=crop",
                "https://images.unsplash.com/photo-1600566753086-00f18fb6b3ea?w=800&h=600&fit=crop",
                "https://images.unsplash.com/photo-1600566752355-35792bedcfea?w=800&h=600&fit=crop",
                "https://images.unsplash.com/photo-1600566753376-12c8ab7fb75b?w=800&h=600&fit=crop"
            };

            for (int i = 0; i < properties.Count; i++)
            {
                images.Add(new PropertyImage
                {
                    IdPropertyImage = Guid.NewGuid().ToString(),
                    IdProperty = properties[i].IdProperty,
                    File = imageUrls[i % imageUrls.Length],
                    Enabled = true,
                    CreatedAt = properties[i].CreatedAt,
                    UpdatedAt = null
                });
            }

            var collection = _database.GetCollection<PropertyImage>("PropertyImages");
            await collection.InsertManyAsync(images);
        }

        private async Task<List<PropertyTrace>> CreatePropertyTraces(List<Property> properties)
        {
            var traces = new List<PropertyTrace>();
            var random = new Random();

            foreach (var property in properties)
            {
                // Create 1-3 traces per property
                var traceCount = random.Next(1, 4);

                for (int i = 0; i < traceCount; i++)
                {
                    var traceDate = property.CreatedAt.AddDays(random.Next(1, 30));
                    var traceValue = property.Price + random.Next(-50000000, 100000000);
                    var tax = CalculateTax(traceValue);

                    traces.Add(new PropertyTrace
                    {
                        IdPropertyTrace = Guid.NewGuid().ToString(),
                        IdProperty = property.IdProperty,
                        DateSale = traceDate,
                        Name = $"Comprador {i + 1} - {property.Name}",
                        Value = traceValue,
                        Tax = tax,
                        CreatedAt = traceDate,
                        UpdatedAt = null
                    });
                }
            }

            var collection = _database.GetCollection<PropertyTrace>("PropertyTraces");
            await collection.InsertManyAsync(traces);
            return traces;
        }

        private decimal CalculateTax(decimal value)
        {
            // Colombian property transfer tax calculation
            const decimal UVT_VALUE = 49700; // UVT value for 2024
            const decimal UVT_20K = 20000 * UVT_VALUE; // 20,000 UVT threshold
            const decimal UVT_50K = 50000 * UVT_VALUE; // 50,000 UVT threshold

            if (value < UVT_20K)
                return 0; // No tax for properties under 20,000 UVT

            if (value <= UVT_50K)
            {
                // 1.5% on excess over 20,000 UVT
                var excess = value - UVT_20K;
                return excess * 0.015m;
            }
            else
            {
                // 3% on excess over 50,000 UVT + 450 UVT fixed
                var excess = value - UVT_50K;
                return (excess * 0.03m) + (450 * UVT_VALUE);
            }
        }
    }
}
