using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace InstantDelivery.Tests
{
    public class EmployeeServiceTests
    {
        private Employee employee = new Employee
        {
            Id = 1,
            FirstName = "A",
            LastName = "B"
        };

        [Fact]
        public void RemoveEmployee_ForDeliveredPackage_ShouldNotChangeItsStatus()
        {
            var package = new Package
            {
                Id = 1,
                Status = PackageStatus.Delivered
            };
            var packages = new List<Package> { package }.AsQueryable();
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);

            var employees = new List<Employee> { employee }.AsQueryable();
            employee.Packages.Add(package);
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            var service = new EmployeeService(mockContext.Object);

            service.RemoveEmployee(employee);

            Assert.Equal(PackageStatus.Delivered, package.Status);
        }

        [Fact]
        public void RemoveEmployee_ForPackagesInDelivery_ShouldSetPackagesStatusBackToNew()
        {
            var package = new Package
            {
                Id = 1,
                Status = PackageStatus.InDelivery
            };
            var packages = new List<Package> { package }.AsQueryable();
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);

            var employees = new List<Employee> { employee }.AsQueryable();
            employee.Packages.Add(package);
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            var service = new EmployeeService(mockContext.Object);

            service.RemoveEmployee(employee);

            Assert.Equal(PackageStatus.New, package.Status);
        }

        [Fact]
        public void AddEmployee_ForAnyValidatedEmployee_ShouldAlwaysAddEmployee()
        {
            var employees = new List<Employee>().AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);
            var employeeToAdd = new Employee() { FirstName = "Ted", LastName = "Mosby" };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);
            var service = new EmployeeService(mockContext.Object);

            service.AddEmployee(employeeToAdd);

            employeesMockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void GetAllEmployees_ShouldReturnAllEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee() { FirstName = "J.D", LastName = "Kyle" } ,
                new Employee() { FirstName = "Ted", LastName = "Mosby"},
                new Employee() { FirstName = "Robin", LastName = "Scherbatsky"}
            }
            .AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);
            var service = new EmployeeService(mockContext.Object);

            var result = service.GetAll();
            var count = result.Count();
            Assert.Equal(count, 3);
        }

        [Fact]
        public void ReloadEmployee_ShouldReloadEmployeeData()
        {

            using (var context = new InstantDeliveryContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var selected = new Employee() { FirstName = "Ted", LastName = "Mosby" };
                    var service = new EmployeeService(context);
                    context.Employees.Add(selected);
                    context.SaveChanges();

                    selected.FirstName = "Robin";
                    selected.LastName = "Scherbatsky";
                    service.Reload(selected);

                    Assert.Equal(selected.FirstName, "Ted");
                    Assert.Equal(selected.LastName, "Mosby");
                }
            }
        }

        [Fact]
        public void ChangeEmployeesVehicle_ShouldChangeEmployeeVehicle()
        {
            var employees = new List<Employee>
            {
                new Employee() { FirstName = "Ted", LastName = "Mosby"},
            }
            .AsQueryable();
            var vehicles = new List<Vehicle>
            {
                new Vehicle() {Id = 1}
            }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);
            var vehiclesMockSet = MockDbSetHelper.GetMockSet(vehicles);
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);
            var service = new EmployeeService(mockContext.Object);
            var selected = employeesMockSet.Object.FirstOrDefault();
            if (selected == null) return;
            employeesMockSet.Object.Attach(selected);
            var vehicle = vehiclesMockSet.Object.FirstOrDefault();
            if (vehicle == null) return;
            vehiclesMockSet.Object.Attach(vehicle);
            service.ChangeEmployeesVehicle(selected, vehicle);

            var result = employeesMockSet.Object.FirstOrDefault();
            if (result != null) Assert.Equal(result.Vehicle.Id, 1);
        }

        [Fact]
        public void SaveEmployee_ShouldSaveEmployee()
        {
            var employees = new List<Employee>
            {
                new Employee() {FirstName = "Ted", LastName = "Mosby"},
            }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);
            var service = new EmployeeService(mockContext.Object);
            var selected = employeesMockSet.Object.FirstOrDefault();
            if (selected == null) return;
            employeesMockSet.Object.Attach(selected);
            selected.FirstName = "Robin";
            selected.LastName = "Scherbatsky";
            service.Save();

            var result = employeesMockSet.Object.FirstOrDefault();
            if (result != null)
            {
                Assert.Equal(result.FirstName, "Robin");
                Assert.Equal(result.LastName, "Scherbatsky");
            }
        }
    }
}
