using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model.Statistics;
using InstantDelivery.Service;
using InstantDelivery.Service.Controllers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Xunit;

namespace InstantDelivery.Tests
{

    public class StatisticsServiceTests
    {
        public StatisticsServiceTests()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [Fact]
        public void Finance_ReturnCorrectTotalPackagesValue()
        {
            var packages = new List<Package>
            {
                new Package {Id = 1, Cost = 1},
                new Package {Id = 2, Cost = 3},
                new Package {Id = 3, Cost = 5}
            };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(packages);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(new List<Employee>());

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.Finance() as OkNegotiatedContentResult<FinancialStatisticsDto>;
            Assert.Equal(result?.Content.TotalPackagesValue, 9);
        }

        [Fact]
        public void Finance_ReturnsCorrectTotalEmployeesSalaries()
        {
            var employees = new List<Employee>
            {
                new Employee {Id = 1, Salary = 1000},
                new Employee {Id = 2, Salary = 3},
                new Employee {Id = 3, Salary = 5000}
            };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employees);
            mockContext.Setup(c => c.Packages).ReturnsDbSet(new List<Package>());

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.Finance() as OkNegotiatedContentResult<FinancialStatisticsDto>;
            Assert.Equal(result?.Content.TotalEmployeesSalaries, 6003);
        }

        [Fact]
        public void Finance_ReturnsCorrectTaxesSum()
        {
            var employees = new List<Employee>
            {
                new Employee {Id = 1, Salary = 1000},
                new Employee {Id = 2, Salary = 3},
                new Employee {Id = 3, Salary = 5000}
            };
            var packages = new List<Package>
            {
                new Package {Id = 1, Cost = 1},
                new Package {Id = 2, Cost = 3},
                new Package {Id = 3, Cost = 5}
            };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employees);
            mockContext.Setup(c => c.Packages).ReturnsDbSet(packages);

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.Finance() as OkNegotiatedContentResult<FinancialStatisticsDto>;
            Assert.Equal(result?.Content.TotalTaxes, 2403.45M);
        }

        [Fact]
        public void General_ReturnsNumberOfEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee {Id = 1},
                new Employee {Id = 2},
                new Employee {Id = 3},
            };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employees);
            mockContext.Setup(c => c.Packages).ReturnsDbSet(new List<Package>());
            mockContext.Setup(c => c.Vehicles).ReturnsDbSet(new List<Vehicle>());

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result?.Content.EmployeesCount, 3);
        }

        [Fact]
        public void General_ReturnsCorrectNumberOfVehicles()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle {Id = 1},
                new Vehicle {Id = 2},
                new Vehicle {Id = 3}
            };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(new List<Employee>());
            mockContext.Setup(c => c.Packages).ReturnsDbSet(new List<Package>());
            mockContext.Setup(c => c.Vehicles).ReturnsDbSet(vehicles);

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result?.Content.AllVehiclesCount, 3);
        }

        [Fact]
        public void General_UnassignedPackages()
        {
            var packages = new List<Package>
            {
                new Package {Id = 1, Cost = 1},
                new Package {Id = 2, Cost = 3}
            };
            var employees = new List<Employee>
            {
                new Employee {Id = 1, Packages = new List<Package> {packages.First()}},
                new Employee {Id = 2 }
            };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employees);
            mockContext.Setup(c => c.Vehicles).ReturnsDbSet(new List<Vehicle>());
            mockContext.Setup(c => c.Packages).ReturnsDbSet(packages);

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result?.Content.UnassignedPackages, 1);
        }

        [Fact]
        public void General_AssignedPackages()
        {
            var packages = new List<Package>
            {
                new Package {Id = 1, Cost = 1},
                new Package {Id = 2, Cost = 3},
                new Package {Id = 3, Cost = 5}
            };
            var employees = new List<Employee>
            {
                new Employee {Id = 1, Packages = new List<Package> {packages[0]}},
                new Employee {Id = 2 , Packages = new List<Package> {packages[1]} }
            };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employees);
            mockContext.Setup(c => c.Vehicles).ReturnsDbSet(new List<Vehicle>());
            mockContext.Setup(c => c.Packages).ReturnsDbSet(packages);

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result?.Content.AssignedPackages, 2);
        }

        [Fact]
        public void NumberOfAllPackages_ReturnsNumberOfAllPackages()
        {
            var packages = new List<Package>
            {
                new Package {Id = 1},
                new Package {Id = 2},
                new Package {Id = 3}
            };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(packages);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(new List<Employee>());
            mockContext.Setup(c => c.Vehicles).ReturnsDbSet(new List<Vehicle>());

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result?.Content.AllPackagesCount, 3);
        }

        [Fact]
        public void General_NumberOfUnusedVehicles()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle {Id = 1},
                new Vehicle {Id = 2},
                new Vehicle {Id = 3}
            };
            var employees = new List<Employee>
            {
                new Employee {Id = 1, Vehicle = vehicles[0]},
                new Employee {Id = 2 , Vehicle = vehicles[1]}
            };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employees);
            mockContext.Setup(c => c.Vehicles).ReturnsDbSet(vehicles);
            mockContext.Setup(c => c.Packages).ReturnsDbSet(new List<Package>());

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result?.Content.UnusedVehicles, 1);
        }

        [Fact]
        public void General_NumberOfUsedVehicles()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle {Id = 1},
                new Vehicle {Id = 2},
                new Vehicle {Id = 3}
            };
            var employees = new List<Employee>
            {
                new Employee {Id = 1, Vehicle = vehicles[0]},
                new Employee {Id = 2 , Vehicle = vehicles[1]}
            };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employees);
            mockContext.Setup(c => c.Vehicles).ReturnsDbSet(vehicles);
            mockContext.Setup(c => c.Packages).ReturnsDbSet(new List<Package>());

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result?.Content.UsedVehicles, 2);
        }
    }
}