using InstantDelivery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace InstantDelivery.Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<InstantDeliveryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "InstantDelivery.Core.InstantDeliveryContext";
        }

        protected override void Seed(InstantDeliveryContext context)
        {
            GenerateTestData(context);
            context.SaveChanges();
        }

        private static void GenerateTestData(InstantDeliveryContext context)
        {
            GenerateTestEmployees(context);
            GenerateTestVehicleModels(context);
            GenerateTestVehicles(context);
            GenerateTestPackages(context);

            GeneratePackageEmployeeRelations(context);
            GenerateVehicleVehicleModelRelations(context);
            GenerateEmployeeVehicleRelations(context);
        }

        private static void GenerateEmployeeVehicleRelations(InstantDeliveryContext context)
        {
            for (var i = 1; i < 30; i++)
            {
                var i1 = i;
                var firstOrDefault = context.Employees.FirstOrDefault(e => e.Id == i1);
                if (firstOrDefault != null)
                    firstOrDefault.Vehicle = context.Vehicles.FirstOrDefault(e => e.Id == i1);
            }
        }

        private static void GenerateVehicleVehicleModelRelations(InstantDeliveryContext context)
        {
            var randomNumber = new Random();
            for (var i = 0; i < 30; i++)
            {
                var next = randomNumber.Next();
                var i1 = i + 1;
                var firstOrDefault = context.Vehicles.FirstOrDefault(e => e.Id == i1);
                if (firstOrDefault != null)
                    firstOrDefault.VehicleModel = context.VehicleModels.FirstOrDefault(e => e.Id == ((next % 10) + 1));
            }
        }

        private static void GeneratePackageEmployeeRelations(InstantDeliveryContext context)
        {
            for (var i = 1; i < 30; i++)
            {
                var i1 = i;
                var firstOrDefault = context.Employees.FirstOrDefault(e => e.Id == i1);
                if (firstOrDefault != null)
                    firstOrDefault.Packages = new List<Package>() { context.Packages.FirstOrDefault(e => e.Id == i1) };
            }
            var orDefault = context.Employees.FirstOrDefault(e => e.Id == 30);
            if (orDefault != null)
                orDefault.Packages = new List<Package>()
                {
                    context.Packages.FirstOrDefault(e => e.Id ==31),
                    context.Packages.FirstOrDefault(e => e.Id ==32),
                };
            var employee = context.Employees.FirstOrDefault(e => e.Id == 31);
            if (employee != null)
                employee.Packages = new List<Package>()
                {
                    context.Packages.FirstOrDefault(e => e.Id ==33),
                    context.Packages.FirstOrDefault(e => e.Id ==34),
                    context.Packages.FirstOrDefault(e => e.Id ==35),
                };
        }

        private static void GenerateTestPackages(InstantDeliveryContext context)
        {
            var randomNumber = new Random();
            var testPackages = new List<Package>();
            for (var i = 0; i < 40; i++)
            {
                var tmp = new Package
                {
                    Height = randomNumber.Next() % 100,
                    Id = i + 1,
                    ShippingAddress =
                    new Address()
                    {
                        City = "Warsaw",
                        Country = "Poland",
                        Number = (randomNumber.Next() % 500).ToString(),
                        PostalCode = ((randomNumber.Next() % 99 + 1).ToString() + "-" + (randomNumber.Next() % 999 + 1)),
                        State = randomNumber.Next().ToString(),
                        Street = randomNumber.Next().ToString(),
                    },
                    Weight = randomNumber.Next() % 100,
                    Width = randomNumber.Next() % 100,
                    Length = randomNumber.Next() % 100,
                    Cost = randomNumber.Next() % 10000
                };
                testPackages.Add(tmp);
            }
            context.Packages.AddOrUpdate(testPackages.ToArray());
        }

        private static void GenerateTestVehicleModels(InstantDeliveryContext context)
        {
            var randomNumber = new Random();
            var brands = new[] { "Opel", "Fiat", "Audi", "Toyota", "Mercedez", "Opel", "Fiat", "Audi", "Toyota", "Mercedez" };
            var models = new[] { "Astra", "Jakiś", "A4", "Yaris", "Benz", "Vectra", "Punto", "A6", "Corolla", "Classic" };

            var testVehicleModels = new List<VehicleModel>();
            for (var i = 0; i < 10; i++)
            {
                testVehicleModels.Add(new VehicleModel()
                {
                    Brand = brands[i],
                    Model = models[i],
                    Payload = (double)(randomNumber.Next() % 1000),
                    Id = i + 1,
                    AvailableSpaceX = (double)(randomNumber.Next() % 1000),
                    AvailableSpaceY = (double)(randomNumber.Next() % 1000),
                    AvailableSpaceZ = (double)(randomNumber.Next() % 1000),
                });
            }
            context.VehicleModels.AddOrUpdate(testVehicleModels.ToArray());
        }

        private static void GenerateTestVehicles(InstantDeliveryContext context)
        {
            var randomNumber = new Random();
            var testVehicle = new List<Vehicle>();
            for (var i = 0; i < 30; i++)
            {
                testVehicle.Add(new Vehicle()
                {
                    RegistrationNumber = "WWW" + (randomNumber.Next() % 100 + 4000).ToString() + "PL",
                    Id = i + 1,
                });
            }
            context.Vehicles.AddOrUpdate(testVehicle.ToArray());
        }

        private static void GenerateTestEmployees(InstantDeliveryContext context)
        {
            var firstName = new[]
            {
                "Jessie", "Jessie", "Justin", "Britney", "Ariana", "David", "Adam", "Enrique", "Ashton", "Lisa", "Bruno", "Jan",
                "Denis", "Richard", "Witold", "Cezary", "Bartłomiej", "Anastazja", "Mariusz", "Janusz", "Helena", "Dorota",
                "Anna", "Henryk", "Markus", "Łukasz", "Mateusz", "Adrian", "Michał", "Ted", "Robin", "Marshall", "Lily",
                "Barney", "Brad", "Elliot", "John", "Jan", "Norbert", "Damian"
            };

            var lastName = new[]
            {
                "James", "Rogers", "Timberlake", "Spears", "Grande", "Beckham", "Levine", "Iglesias", "Kutcher", "Row", "Mars",
                "Andrzejewski", "Rozrabiaka", "Marx", "Tacikiewicz", "Żak", "Dąbrowski", "Bąk", "Kolonko", "Swojski", "Romański",
                "Zawadzka", "Mucha", "Sienkiewicz", "Ellen", "Jakóbiak", "Piasecki", "Dudek", "Nachtman", "Mosby", "Scherbatsky",
                "Erickson", "Aldrin", "Stinson", "Pitt", "Reid", "Dorian", "Andrzejewski", "Gierczak", "Damil"
            };
            var randomNumber = new Random();
            var testEmployees = new List<Employee>();
            DateTime startDate = new DateTime(1950, 1, 1);
            int daysUntilNow = (DateTime.Now - startDate).Days;
            for (var i = 0; i < 100; i++)
            {
                testEmployees.Add(new Employee
                {
                    Id = i + 1,
                    FirstName = firstName[randomNumber.Next() % firstName.Length],
                    LastName = lastName[randomNumber.Next() % lastName.Length],
                    Gender = (Gender)(randomNumber.Next() % 2),
                    PlaceOfResidence = new Address { City = "Warsaw", Country = "Poland", Number = "2", PostalCode = "23-456", State = "Virdżinia", Street = "alalal" },
                    DateOfBirth = startDate.AddDays(randomNumber.Next() % daysUntilNow),
                    PhoneNumber =
                        (randomNumber.Next() % 300) + 700 + (randomNumber.Next() % 300 + 700).ToString() +
                        (randomNumber.Next() % 300 + 700),
                    Salary = randomNumber.Next() % 1000 + 5000,
                    HireDate = startDate.AddDays(randomNumber.Next() % daysUntilNow)
                });
            }

            context.Employees.AddOrUpdate(testEmployees.ToArray());
        }
    }
}
