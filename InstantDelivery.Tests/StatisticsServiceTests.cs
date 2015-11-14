using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
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

            var statisticsService = new StatisticsService(mockContext.Object);
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            var result = statisticsService.ValueOfAllPackages();
            Assert.Equal(result, 0);
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
            var statisticsService = new StatisticsService(mockContext.Object);

            var result = statisticsService.EmployeesSalaries();
            Assert.Equal(result, 6003);
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
            var statisticsService = new StatisticsService(mockContext.Object);

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


            var result = statisticsService.Taxes(9, 7500);
            Assert.Equal(result, 3002);
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
            var statisticsService = new StatisticsService(mockContext.Object);

            var result = statisticsService.NumberOfEmployees();
            Assert.Equal(result, 3);
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
            var statisticsService = new StatisticsService(mockContext.Object);

            var result = statisticsService.NumberOfVehicles();
            Assert.Equal(result, 3);
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
            var statisticsService = new StatisticsService(mockContext.Object);

            var result = statisticsService.NumberOfPackagesWithEmployee();
            Assert.Equal(result, 2);
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
            var statisticsService = new StatisticsService(mockContext.Object);

            var result = statisticsService.NumberOfPackagesWithEmployee();
            Assert.Equal(result, 2);
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
            var statisticsService = new StatisticsService(mockContext.Object);

            var result = statisticsService.NumberOfAllPackages();
            Assert.Equal(result, 3);
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

            var statisticsService = new StatisticsService(mockContext.Object);

            var result = statisticsService.NumberOfUsedVehicles();
            Assert.Equal(result, 1);
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

            var statisticsService = new StatisticsService(mockContext.Object);

            var result = statisticsService.NumberOfUnusedVehicles();
            Assert.Equal(result, 3);
        }
    }
}