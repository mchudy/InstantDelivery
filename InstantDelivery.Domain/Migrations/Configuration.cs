using InstantDelivery.Common.Enums;
using InstantDelivery.Common.Extensions;
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
        /// <summary>
        /// Konstruktor konfiguratora bazy danych.
        /// </summary>
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

        private static readonly Random random = new Random();

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
            "Nowy Świat",
            "Puławska",
            "Jana Pawła II",
            "Emilii Plater",
            "Aleja Waszyngtona",
            "Aleja Solidarności",
            "Jagiellońska",
            "Powstańców Śląskich",
            "Aleja Wilanowska",
            "Żelazna",
            "Obozowa",
            "Górczewska",
            "Łopuszańska",
            "Aleja Krakowska",
            "Ostrobramska",
            "Grójecka"
        };

        private static readonly string[] states =
        {
            "mazowieckie",
            "pomorskie",
            "małopolskie"
        };

        private static readonly string[] firstNames = {
                "Jessie", "Jessie", "Justin", "Britney", "Ariana", "David", "Adam", "Enrique", "Ashton", "Lisa", "Bruno",
                "Jan",
                "Denis", "Richard", "Witold", "Cezary", "Bartłomiej", "Anastazja", "Mariusz", "Janusz", "Helena",
                "Dorota",
                "Anna", "Henryk", "Markus", "Łukasz", "Mateusz", "Adrian", "Michał", "Ted", "Robin", "Marshall", "Lily",
                "Barney", "Brad", "Elliot", "John", "Jan", "Norbert", "Damian"
        };

        private static readonly string[] lastNames = {
                "James", "Rogers", "Timberlake", "Spears", "Grande", "Beckham", "Levine", "Iglesias", "Kutcher", "Row",
                "Mars",
                "Andrzejewski", "Rozrabiaka", "Marx", "Tacikiewicz", "Żak", "Dąbrowski", "Bąk", "Kolonko", "Swojski",
                "Romański",
                "Zawadzka", "Mucha", "Sienkiewicz", "Ellen", "Jakubiak", "Piasecki", "Dudek", "Nachtman", "Mosby",
                "Scherbatsky",
                "Erickson", "Aldrin", "Stinson", "Pitt", "Reid", "Dorian", "Andrzejewski", "Gierczak", "Damil"
            };

        private static void GenerateTestData(InstantDeliveryContext context)
        {
            GenerateRoles(context);
            GenerateTestEmployees(context);
            GenerateCustomers(context);
            GenerateUsers(context);
            GenerateTestVehicleModels(context);
            GenerateTestVehicles(context);
            GenerateTestPackages(context);

            SetPackagesInDelivery(context);
            SetDeliveredPackages(context);
            GenerateVehicleVehicleModelRelations(context);
            GenerateEmployeeVehicleRelations(context);
            GenerateCustomersPackageRelations(context);
        }

        private static void GenerateCustomersPackageRelations(InstantDeliveryContext context)
        {
            var customer = context.Customers.FirstOrDefault(c => c.User.UserName == "customer");
            var package = context.Packages.FirstOrDefault();
            customer?.Packages.Add(package);
        }

        private static void GenerateCustomers(InstantDeliveryContext context)
        {
            var customers = new List<Customer>();
            DateTime startDate = new DateTime(1950, 1, 1);
            int daysUntilNow = (DateTime.Now - startDate).Days;
            for (var i = 0; i < 10; i++)
            {
                var city = cities[random.Next() % cities.Length];
                customers.Add(new Customer
                {
                    Id = i + 1,
                    FirstName = firstNames[random.Next() % firstNames.Length],
                    LastName = lastNames[random.Next() % lastNames.Length],
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
                    Rank = Rank.Beginner,
                    DateOfBirth = startDate.AddDays(random.Next() % daysUntilNow),
                    PhoneNumber =
                        (random.Next() % 300) + 700 + (random.Next() % 300 + 700).ToString() +
                        (random.Next() % 300 + 700)
                });
            }

            context.Customers.AddOrUpdate(customers.ToArray());
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

        private struct UserData
        {
            public UserData(User user, string password, Role role)
            {
                User = user;
                Password = password;
                Role = role;
            }

            public User User { get; }
            public string Password { get; }
            public Role Role { get; }
        }

        private static void GenerateUsers(InstantDeliveryContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            for (int i = 1; i < 50; i++)
            {
                var employee = context.Employees.Find(i);
                var username = i == 1 ? "courier" : GenerateUserName(employee.FirstName, employee.LastName, context);
                var user = new User { UserName = username };
                if (AddUser(context, userManager, new UserData(user, "courier123", Role.Courier)))
                {
                    employee.User = user;
                }
            }

            for (int i = 51; i < 90; i++)
            {
                var employee = context.Employees.Find(i);
                var username = i == 51 ? "employee" : GenerateUserName(employee.FirstName, employee.LastName, context);
                var user = new User { UserName = username };
                if (AddUser(context, userManager, new UserData(user, "employee123", Role.AdministrativeEmployee)))
                {
                    employee.User = user;
                }
            }

            for (int i = 91; i <= 100; i++)
            {
                var employee = context.Employees.Find(i);
                var username = i == 91 ? "admin" : GenerateUserName(employee.FirstName, employee.LastName, context);
                var user = new User { UserName = username };
                if (AddUser(context, userManager, new UserData(user, "admin123", Role.Admin)))
                {
                    employee.User = user;
                }
            }

            for (int i = 1; i <= 10; i++)
            {
                var customer = context.Customers.Find(i);
                var username = i == 1 ? "customer" : GenerateUserName(customer.FirstName, customer.LastName, context);
                var user = new User { UserName = username };
                if (AddUser(context, userManager, new UserData(user, "customer123", Role.Customer)))
                {
                    customer.User = user;
                }
            }
        }

        private static bool AddUser(InstantDeliveryContext context, UserManager<User> userManager, UserData userData)
        {
            if (!context.Users.Any(u => userData.User.UserName == u.UserName))
            {
                var result = userManager.Create(userData.User, userData.Password);
                if (!result.Succeeded)
                {
                    var results =
                        result.Errors.Select(
                            e => new DbEntityValidationResult(context.Entry(userData.User), new[] { new DbValidationError("", e) }));
                    throw new DbEntityValidationException("", new List<DbEntityValidationResult>(results));
                }
                userManager.AddToRole(userData.User.Id, userData.Role.ToString());
                return true;
            }
            return false;
        }


        private static string GenerateUserName(string firstName, string lastName, InstantDeliveryContext context)
        {
            string username = (firstName + lastName).ToLower();
            username = username.ReplaceNationalCharacters();
            if (context.Users.Any(u => u.UserName == username))
            {
                int i = 1;
                while (context.Users.Any(u => u.UserName == username + i))
                {
                    i++;
                }
                username = username + i;
            }
            return username;
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

        private static void SetPackagesInDelivery(InstantDeliveryContext context)
        {
            for (int i = 1; i < 150; i++)
            {
                var package = context.Packages.Find(i);
                if (package != null)
                {
                    int employeeId = random.Next() % 20;
                    var employee = context.Employees.Find(employeeId);
                    if (employee != null && package.Status == PackageStatus.InWarehouse)
                    {
                        employee.Packages.Add(package);
                        package.Status = PackageStatus.InDelivery;
                        context.PackageEvents.Add(new PackageEvent
                        {
                            EventType = PackageEventType.HandedToCourier,
                            Package = package,
                            Employee = employee
                        });
                    }
                }
            }
        }

        private static void SetDeliveredPackages(InstantDeliveryContext context)
        {
            for (int i = 100; i < 150; i++)
            {
                var package = context.Packages.Find(i);
                if (package != null)
                {
                    var employee = context.Employees.FirstOrDefault(e => e.Packages.Any(p => p.Id == package.Id));
                    if (employee != null && package.Status == PackageStatus.InDelivery)
                    {
                        employee.Packages.Remove(package);
                        package.Status = PackageStatus.Delivered;
                        context.PackageEvents.Add(new PackageEvent
                        {
                            EventType = PackageEventType.Delivered,
                            Package = package,
                            Employee = employee
                        });
                    }
                }
            }
        }

        private static void GenerateTestPackages(InstantDeliveryContext context)
        {
            var testPackages = new List<Package>();
            var packageEvents = new List<PackageEvent>();
            for (var i = 0; i < 200; i++)
            {
                if (context.Packages.Find(i) != null)
                {
                    continue;
                }
                var tmp = new Package
                {
                    Id = i + 1,
                    ShippingAddress =
                        new Address
                        {
                            City = "Warszawa",
                            Country = "Polska",
                            Number = (random.Next() % 50).ToString(),
                            PostalCode = random.Next() % 99 + 1 + "-" + (random.Next() % 999 + 1),
                            State = "mazowieckie",
                            Street = streets[random.Next() % streets.Length],
                        },
                    Weight = random.Next() % 90 + 1,
                    Width = random.Next() % 90 + 1,
                    Length = random.Next() % 90 + 1,
                    Height = random.Next() % 90 + 1,
                    Cost = random.Next() % 1000
                };
                testPackages.Add(tmp);
                packageEvents.Add(new PackageEvent
                {
                    Package = tmp,
                    EventType = PackageEventType.RegisteredInWarehouse,
                });
            }
            context.Packages.AddOrUpdate(testPackages.ToArray());
            context.PackageEvents.AddOrUpdate(packageEvents.ToArray());
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
            var testEmployees = new List<Employee>();
            DateTime startDate = new DateTime(1950, 1, 1);
            int daysUntilNow = (DateTime.Now - startDate).Days;
            for (var i = 0; i < 100; i++)
            {
                var city = cities[random.Next() % cities.Length];
                testEmployees.Add(new Employee
                {
                    Id = i + 1,
                    FirstName = firstNames[random.Next() % firstNames.Length],
                    LastName = lastNames[random.Next() % lastNames.Length],
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
