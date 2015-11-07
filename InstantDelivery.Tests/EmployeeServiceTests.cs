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
    }
}
