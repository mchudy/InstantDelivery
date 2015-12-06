using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model.Statistics;
using InstantDelivery.Service.Controllers;
using InstantDelivery.ViewModel.Proxies;
using Moq;
using Xunit;

namespace InstantDelivery.Tests
{

    public class StatisticsServiceTests
    {
        [Fact]
        public void ValueOfAllPackages_ReturnsSumOfCostsOfPackages()
        {
            var packages = new List<Package> { new Package
            {
                Id = 1,
                Cost=1
            },new Package
            {
                Id = 2,
                Cost=3,
            },new Package
            {
                Id = 3,
                Cost=5
            }
        }.AsQueryable();
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);

            var employees = new List<Employee> { new Employee
            {
                Id = 1,
            },new Employee
            {
                Id = 2,
            },new Employee
            {
                Id = 3,
            }
        }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

            var proxy = new StatisticsController(mockContext.Object);
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            var result = proxy.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
        }

        [Fact]
        public void EmployeesSalaries_ReturnsSumOfAllEmployeesSalaries()
        {
            var employees = new List<Employee> { new Employee
            {
                Id = 1,
                Salary= 1000
            },new Employee
            {
                Id = 1500,
                Salary=3
            },new Employee
            {
                Id = 1,
                Salary=5000
            }
        }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);
            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<FinancialStatisticsDto>;
            Assert.Equal(result.Content.TotalEmployeesSalaries, 6003);
        }

        [Fact]
        public void Taxes_ReturnsValueOfTaxes()
        {
            var employees = new List<Employee> { new Employee
            {
                Id = 1,
                Salary= 1000
            },new Employee
            {
                Id = 1500,
                Salary=3
            },new Employee
            {
                Id = 2,
                Salary=5000
            }
        }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            var packages = new List<Package> { new Package
            {
                Id = 1,
                Cost=1
            },new Package
            {
                Id = 2,
                Cost=3
            },new Package
            {
                Id = 1,
                Cost=5
            }
        }.AsQueryable();
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);

            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.Finance() as OkNegotiatedContentResult<FinancialStatisticsDto>;
            Assert.Equal(result.Content.TotalTaxes, 3002);
        }

        [Fact]
        public void NumberOfEmployees_ReturnsNumberOfEmployees()
        {
            var employees = new List<Employee> { new Employee
            {
                Id = 1,
            },new Employee
            {
                Id = 2,
            },new Employee
            {
                Id = 3,
            }
        }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);
            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result.Content.EmployeesCount, 3);
        }

        [Fact]
        public void NumberOfVehicles_ReturnsNumberOfVehicles()
        {
            var vehicles = new List<Vehicle> { new Vehicle
            {
                Id = 1,
            },new Vehicle
            {
                Id = 2,
            },new Vehicle
            {
                Id = 3,
            }
        }.AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.GetMockSet(vehicles);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);
            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result.Content.AllVehiclesCount, 3);
        }

        [Fact]
        public void NumberOfPackagesWithEmployee_ReturnsNumberOfPackagesWithEmployee()
        {
            var packages = new List<Package> { new Package
            {
                Id = 1,
                Cost=1,

            },new Package
            {
                Id = 2,
                Cost=3
            }
        }.AsQueryable();


            var employees = new List<Employee> { new Employee
            {
                Id = 1,
                Packages = new List<Package>() { packages.First()}
            },new Employee
            {
                Id = 2,
                Packages = new List<Package>() { packages.Last()}
            },new Employee
            {
                Id = 3,
            }
        }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result.Content.UnassignedPackages, 2);
        }

        [Fact]
        public void NumberOfPackagesWithoutEmployee_ReturnsNumberOfPackagesWithEmployee()
        {
            var packages = new List<Package> { new Package
            {
                Id = 1,
                Cost=1,

            },new Package
            {
                Id = 2,
                Cost=3
            },new Package
            {
                Id = 1,
                Cost=5
            }
        }.AsQueryable();


            var employees = new List<Employee> { new Employee
            {
                Id = 1,
                Packages = new List<Package>() { packages.FirstOrDefault()}
            },new Employee
            {
                Id = 2,
            },new Employee
            {
                Id = 3,
            }
        }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result.Content.AssignedPackages, 2);
        }

        public void NumberOfAllPackages_ReturnsNumberOfAllPackages()
        {
            var packages = new List<Package> { new Package
            {
                Id = 1,
                Cost=1
            },new Package
            {
                Id = 2,
                Cost=3
            },new Package
            {
                Id = 1,
                Cost=5
            }
        }.AsQueryable();
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);
            var mockContext = new Mock<InstantDeliveryContext>();

            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result.Content.AllPackagesCount, 3);
        }

        [Fact]
        public void NumberOfUsedVehicles_ReturnsNumberOfUsedVehicles()
        {
            var vehicles = new List<Vehicle> { new Vehicle
            {
                Id = 1,
            },new Vehicle
            {
                Id = 2,
            },new Vehicle
            {
                Id = 3,
            }
        }.AsQueryable();


            var employees = new List<Employee> { new Employee
            {
                Id = 1,
                Vehicle=vehicles.First()
            },new Employee
            {
                Id = 2,
                Vehicle=new Vehicle()
            },new Employee
            {
                Id = 3,
                Vehicle = new Vehicle()
            }
        }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);
            var vehiclesMockSet = MockDbSetHelper.GetMockSet(vehicles);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);

            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            Assert.Equal(result.Content.UnusedVehicles, 1);
        }

        [Fact]
        public void NumberOfUnusedVehicles_ReturnsNumberOfUnusedVehicles()
        {
            var vehicles = new List<Vehicle> { new Vehicle
            {
                Id = 1,
            },new Vehicle
            {
                Id = 2,
            },new Vehicle
            {
                Id = 3,
            }
        }.AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.GetMockSet(vehicles);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);


            var employees = new List<Employee> { new Employee
            {
                Id = 1,
                Vehicle = new Vehicle()
            },new Employee
            {
                Id = 2,
                Vehicle = new Vehicle()
            },new Employee
            {
                Id = 3,
                Vehicle = new Vehicle()
            }
        }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            var controller = new StatisticsController(mockContext.Object);

            var result = controller.General() as OkNegotiatedContentResult<GeneralStatisticsDto>;
            if (result != null) Assert.Equal(result.Content.UnusedVehicles, 3);
        }
    }
}