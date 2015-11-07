using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using InstantDelivery.Services;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace InstantDelivery.Tests
{
    public class PackageServiceTests
    {
        [Fact]
        public void RegisterPackage_AddsPackageToDatabase()
        {
            var mockSet = new Mock<DbSet<Package>>();
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(m => m.Packages).Returns(mockSet.Object);
            var mockPricingStrategy = new Mock<IPricingStrategy>();
            var service = new PackageService(mockContext.Object, mockPricingStrategy.Object);
            var package = new Package()
            {
                Width = 100,
                Height = 200,
                Length = 150,
                Weight = 100
            };

            service.RegisterPackage(package);

            mockSet.Verify(m => m.Add(package), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void RegisterPackage_SetsStatusToNew()
        {
            var mockContext = GetEmptyMockContext();
            var mockPricingStrategy = new Mock<IPricingStrategy>();
            var service = new PackageService(mockContext.Object, mockPricingStrategy.Object);
            var package = new Package();

            service.RegisterPackage(package);

            Assert.Equal(PackageStatus.New, package.Status);
        }

        [Fact]
        public void RegisterPackage_AssignsCostToThePackage()
        {
            var mockContext = GetEmptyMockContext();
            var mockPricingStrategy = new Mock<IPricingStrategy>();
            mockPricingStrategy.Setup(m => m.GetCost(It.IsAny<Package>())).Returns(10M);
            var service = new PackageService(mockContext.Object, mockPricingStrategy.Object);
            var package = new Package();

            service.RegisterPackage(package);

            Assert.Equal(10M, package.Cost);
        }


        [Fact]
        public void AssignPackage_AssignsPackageToEmployee()
        {
            var package = new Package
            {
                Id = 1,
                Height = 100,
                Weight = 100,
                Width = 100,
                Length = 100,
                Status = PackageStatus.New
            };
            var packages = new List<Package> { package }.AsQueryable();
            var packagesMockSet = GetMockSet(packages);

            var employee = new Employee
            {
                Id = 1,
                FirstName = "A",
                LastName = "B"
            };
            var employees = new List<Employee> { employee }.AsQueryable();
            var employeesMockSet = GetMockSet(employees);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            var service = new PackageService(mockContext.Object, null);

            service.AssignPackage(package, employee);

            Assert.Equal(PackageStatus.InDelivery, package.Status);
            Assert.Equal(1, employee.Packages.Count);
            Assert.Equal(package, employee.Packages.First());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        private static Mock<InstantDeliveryContext> GetEmptyMockContext()
        {
            var mockSet = new Mock<DbSet<Package>>();
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(m => m.Packages).Returns(mockSet.Object);
            return mockContext;
        }

        private static Mock<DbSet<T>> GetMockSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }
    }
}