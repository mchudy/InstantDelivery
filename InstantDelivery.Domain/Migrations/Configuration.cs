using InstantDelivery.Common.Enums;
using InstantDelivery.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace InstantDelivery.Domain.Migrations
{
    /// <summary>
    /// Klasa konfigurująca bazę.
    /// Posiada metody generujące testowe dane.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<InstantDeliveryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "InstantDelivery.Domain.InstantDeliveryContext";
        }

        protected override void Seed(InstantDeliveryContext context)
        {
            try
            {
                GenerateTestData(context);
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure?.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException("Could not add entity to the database:\n" + sb, ex);
            }
        }

        private static Random random = new Random();

        private static readonly string[][] cities =
        {
            new[] {"Kraków", "Polska"},
            new[] {"Warszawa", "Polska"},
            new[] {"Poznań", "Polska"},
            new[] {"Gdańsk", "Polska"},
            new[] {"Paryż", "Francja"},
            new[] {"Londyn", "Wielka Brytania"},
            new[] {"Barcelona", "Hiszpania"},
        };

        private static readonly string[] streets =
        {
            "Aleje Jerozolimskie",
            "Koszykowa",
            "Noakowskiego",
            "Nowy Świat",
        };

        private static readonly string[] states =
        {
            "mazowieckie",
            "pomorskie",
            "małopolskie"
        };

        private static void GenerateTestData(InstantDeliveryContext context)
        {
            GenerateRoles(context);
            GenerateTestEmployees(context);
            GenerateUsers(context);
            GenerateEmployeesUsersRelations(context);
            GenerateTestVehicleModels(context);
            GenerateTestVehicles(context);
            GenerateTestPackages(context);

            GeneratePackageEmployeeRelations(context);
            GenerateVehicleVehicleModelRelations(context);
            GenerateEmployeeVehicleRelations(context);
        }

        private static void GenerateEmployeesUsersRelations(InstantDeliveryContext context)
        {
            // to repair
            var employee = context.Employees.Find(1);
            if (employee != null)
                employee.User = context.Users.Find("1");
            var employee2 = context.Employees.Find(2);
            if (employee2 != null)
                employee2.User = context.Users.Find("2");
            var employee3 = context.Employees.Find(3);
            if (employee3 != null)
                employee3.User = context.Users.Find("3");


        }

        private static void GenerateRoles(InstantDeliveryContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            foreach (string roleName in typeof(Role).GetEnumNames())
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    roleManager.Create(role);
                }
            }
        }

        private static void GenerateUsers(InstantDeliveryContext context)
        {
            var users = new[]
            {
                new {User = new User {UserName = "admin", Id="1"}, Password = "admin123", Role = Role.Admin},
                new {User = new User {UserName = "employee", Id="2"}, Password = "employee123", Role = Role.AdministrativeEmployee},
                new {User = new User {UserName = "courier", Id="3"}, Password = "courier123", Role = Role.Courier}
            };

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            foreach (var userData in users)
            {
                if (!context.Users.Any(u => userData.User.UserName == u.UserName))
                {
                    var result = userManager.Create(userData.User, userData.Password);
                    if (!result.Succeeded)
                    {
                        var results =
                            result.Errors.Select(
                                e => new DbEntityValidationResult(context.Entry(userData), new[] { new DbValidationError("", e) }));
                        throw new DbEntityValidationException("", new List<DbEntityValidationResult>(results));
                    }
                    userManager.AddToRole(userData.User.Id, userData.Role.ToString());
                }
            }
        }

        private static void GenerateEmployeeVehicleRelations(InstantDeliveryContext context)
        {
            for (var i = 1; i < 30; i++)
            {
                var i1 = i;
                var employee = context.Employees.Find(i1);
                if (employee != null)
                    employee.Vehicle = context.Vehicles.FirstOrDefault(e => e.Id == i1);
            }
        }

        private static void GenerateVehicleVehicleModelRelations(InstantDeliveryContext context)
        {
            var randomNumber = new Random();
            for (var i = 1; i <= 30; i++)
            {
                var next = randomNumber.Next();
                var vehicle = context.Vehicles.Find(i);
                if (vehicle != null)
                    vehicle.VehicleModel = context.VehicleModels.Find((next % 10) + 1);
            }
        }

        private static void GeneratePackageEmployeeRelations(InstantDeliveryContext context)
        {
            for (int i = 0; i < 80; i++)
            {
                var package = context.Packages.Find(i);
                if (package != null)
                {
                    int employeeId = random.Next() % 40;
                    var employee = context.Employees.Find(employeeId);
                    if (employee != null)
                    {
                        employee.Packages.Add(package);
                        package.Status = PackageStatus.InDelivery;
                    }
                }
            }
        }

        private static void GenerateTestPackages(InstantDeliveryContext context)
        {
            var testPackages = new List<Package>();
            for (var i = 0; i < 150; i++)
            {
                var city = cities[random.Next() % cities.Length];
                var tmp = new Package
                {
                    Height = random.Next() % 90 + 1,
                    Id = i + 1,
                    ShippingAddress =
                        new Address()
                        {
                            City = city[0],
                            Country = city[1],
                            Number = (random.Next() % 500).ToString(),
                            PostalCode = ((random.Next() % 99 + 1) + "-" + (random.Next() % 999 + 1)),
                            State = random.Next().ToString(),
                            Street = random.Next().ToString(),
                        },
                    Weight = random.Next() % 90 + 1,
                    Width = random.Next() % 90 + 1,
                    Length = random.Next() % 90 + 1,
                    Cost = random.Next() % 1000,
                    Status = PackageStatus.New
                };
                testPackages.Add(tmp);
            }
            context.Packages.AddOrUpdate(testPackages.ToArray());
        }

        private static void GenerateTestVehicleModels(InstantDeliveryContext context)
        {
            var brands = new[]
            {"Opel", "Ford", "Audi", "Toyota", "Mercedez", "Opel", "Fiat", "Audi", "Toyota", "Mercedes"};
            var models = new[]
            {"Astra", "Transit", "A4", "Yaris", "Benz", "Vectra", "Punto", "A6", "Corolla", "Classic"};

            var testVehicleModels = new List<VehicleModel>();
            for (var i = 0; i < 10; i++)
            {
                testVehicleModels.Add(new VehicleModel()
                {
                    Brand = brands[i],
                    Model = models[i],
                    Payload = random.Next() % 1000,
                    Id = i + 1,
                    AvailableSpaceX = random.Next() % 1000,
                    AvailableSpaceY = random.Next() % 1000,
                    AvailableSpaceZ = random.Next() % 1000,
                });
            }
            context.VehicleModels.AddOrUpdate(testVehicleModels.ToArray());
        }

        private static void GenerateTestVehicles(InstantDeliveryContext context)
        {
            var testVehicle = new List<Vehicle>();
            for (var i = 0; i < 30; i++)
            {
                testVehicle.Add(new Vehicle()
                {
                    RegistrationNumber = "WWW" + (random.Next() % 100 + 4000).ToString() + "PL",
                    Id = i + 1,
                });
            }
            context.Vehicles.AddOrUpdate(testVehicle.ToArray());
        }

        private static void GenerateTestEmployees(InstantDeliveryContext context)
        {
            var firstName = new[]
            {
                "Jessie", "Jessie", "Justin", "Britney", "Ariana", "David", "Adam", "Enrique", "Ashton", "Lisa", "Bruno",
                "Jan",
                "Denis", "Richard", "Witold", "Cezary", "Bartłomiej", "Anastazja", "Mariusz", "Janusz", "Helena",
                "Dorota",
                "Anna", "Henryk", "Markus", "Łukasz", "Mateusz", "Adrian", "Michał", "Ted", "Robin", "Marshall", "Lily",
                "Barney", "Brad", "Elliot", "John", "Jan", "Norbert", "Damian"
            };

            var lastName = new[]
            {
                "James", "Rogers", "Timberlake", "Spears", "Grande", "Beckham", "Levine", "Iglesias", "Kutcher", "Row",
                "Mars",
                "Andrzejewski", "Rozrabiaka", "Marx", "Tacikiewicz", "Żak", "Dąbrowski", "Bąk", "Kolonko", "Swojski",
                "Romański",
                "Zawadzka", "Mucha", "Sienkiewicz", "Ellen", "Jakubiak", "Piasecki", "Dudek", "Nachtman", "Mosby",
                "Scherbatsky",
                "Erickson", "Aldrin", "Stinson", "Pitt", "Reid", "Dorian", "Andrzejewski", "Gierczak", "Damil"
            };
            var testEmployees = new List<Employee>();
            DateTime startDate = new DateTime(1950, 1, 1);
            int daysUntilNow = (DateTime.Now - startDate).Days;
            for (var i = 0; i < 100; i++)
            {
                var city = cities[random.Next() % cities.Length];
                testEmployees.Add(new Employee
                {
                    Id = i + 1,
                    FirstName = firstName[random.Next() % firstName.Length],
                    LastName = lastName[random.Next() % lastName.Length],
                    Gender = (Gender)(random.Next() % 2),
                    PlaceOfResidence = new Address
                    {
                        City = city[0],
                        Country = city[1],
                        Number = (random.Next() % 10).ToString(),
                        PostalCode = "00-34" + (random.Next() % 9),
                        State = states[random.Next() % states.Length],
                        Street = streets[random.Next() % streets.Length]
                    },
                    DateOfBirth = startDate.AddDays(random.Next() % daysUntilNow),
                    PhoneNumber =
                        (random.Next() % 300) + 700 + (random.Next() % 300 + 700).ToString() +
                        (random.Next() % 300 + 700),
                    Salary = random.Next() % 1000 + 5000,
                    HireDate = startDate.AddDays(random.Next() % daysUntilNow)
                });
            }

            context.Employees.AddOrUpdate(testEmployees.ToArray());
        }
    }


}
