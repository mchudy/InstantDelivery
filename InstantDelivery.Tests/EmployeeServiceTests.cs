using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InstantDelivery.Common.Enums;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Service.Controllers;
using Moq;
using Xunit;

namespace InstantDelivery.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Employee employee = new Employee
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

            var controller = new EmployeesController(mockContext.Object);
            // ta baza siedzi w jakimś providerze w Employees, a nie ma jej w result view .. WTF
            controller.Delete(employee.Id);

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

            var controller = new EmployeesController(mockContext.Object);
            controller.Delete(employee.Id);

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
            var controller = new EmployeesController(mockContext.Object);
            var employeeToAddDto = new EmployeeAddDto();
            Mapper.DynamicMap(employeeToAdd, employeeToAddDto);
            controller.Post(employeeToAddDto);
            employeesMockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void ReloadEmployee_ShouldReloadEmployeeData()
        {

            using (var context = new InstantDeliveryContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var mockContext = new Mock<InstantDeliveryContext>();
                    mockContext.Setup(c => c.Employees).Returns(context.Employees);
                    var selected = new Employee() { FirstName = "Ted", LastName = "Mosby" };
                    context.Employees.Add(selected);
                    context.SaveChanges();
                    var controller = new EmployeesController(mockContext.Object);

                    selected.FirstName = "Robin";
                    selected.LastName = "Scherbatsky";
                    Mapper.DynamicMap(controller.Get(selected.Id),selected,typeof(EmployeeDto), typeof(Employee));

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
                new Employee() { FirstName = "Ted", LastName = "Mosby", Id=1},
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
            var selected = employeesMockSet.Object.FirstOrDefault();
            if (selected == null) return;
            employeesMockSet.Object.Attach(selected);
            var vehicle = vehiclesMockSet.Object.FirstOrDefault();
            if (vehicle == null) return;
            vehiclesMockSet.Object.Attach(vehicle);
            var controller = new EmployeesController(mockContext.Object);
            controller.ChangeVehicle(selected.Id, vehicle.Id);

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
            var selected = employeesMockSet.Object.FirstOrDefault();
            if (selected == null) return;
            employeesMockSet.Object.Attach(selected);
            selected.FirstName = "Robin";
            selected.LastName = "Scherbatsky";
            var result = employeesMockSet.Object.FirstOrDefault();
            if (result == null) return;
            Assert.Equal(result.FirstName, "Robin");
            Assert.Equal(result.LastName, "Scherbatsky");
        }
    }
}
