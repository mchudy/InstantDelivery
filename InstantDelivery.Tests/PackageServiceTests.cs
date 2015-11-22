using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Services;
using InstantDelivery.Services.Pricing;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace InstantDelivery.Tests
{

    public class PackageServiceTests
    {
        private Employee employee = new Employee
        {
            Id = 1,
            FirstName = "A",
            LastName = "B"
        };
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
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);

            var employee = new Employee
            {
                Id = 1,
                FirstName = "A",
                LastName = "B"
            };
            var employees = new List<Employee> { employee }.AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

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

        [Fact]
        public void RegisterPackage_ForValidatedPackage_ShouldAddNewPackage()
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
            var packages = new List<Package>().AsQueryable();
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);


            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            var pricingStrategy = new Mock<IPricingStrategy>().Object;
            var service = new PackageService(mockContext.Object, pricingStrategy);

            service.RegisterPackage(package);

            packagesMockSet.Verify(m => m.Add(It.IsAny<Package>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void ReloadPackage_ShouldReloadPackageData()
        {
            using (var context = new InstantDeliveryContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var package = new Package() { Height = 5, Width = 10, Length = 10, Weight = 10, Status = PackageStatus.New };
                    var pricingStrategy = new Mock<IPricingStrategy>().Object;
                    var service = new PackageService(context, pricingStrategy);
                    context.Packages.Add(package);
                    context.SaveChanges();

                    package.Height = 10;
                    service.Reload(package);

                    Assert.Equal(package.Height, 5);
                }
            }
        }

        [Fact]
        public void SavePackage_ShouldSavePackage()
        {
            var packages = new List<Package>() { new Package() { Height = 5 } }.AsQueryable();
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            var pricingStrategy = new Mock<IPricingStrategy>().Object;
            var service = new PackageService(mockContext.Object, pricingStrategy);
            var selected = packagesMockSet.Object.FirstOrDefault();
            if (selected == null) return;
            packagesMockSet.Object.Attach(selected);
            selected.Height = 10;
            service.Save();
            var result = packagesMockSet.Object.FirstOrDefault();
            if (result != null) Assert.Equal(result.Height, 10);
        }


        [Fact]
        public void RemovePackage_ShouldRemovePackageAndDetachItFromEmployee()
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
            var pricingStrategy = new Mock<IPricingStrategy>().Object;
            var packagesService = new PackageService(mockContext.Object, pricingStrategy);
            mockContext.Object.Employees.Attach(employee);
            employeesMockSet.Object.Attach(employee);

            packagesService.RemovePackage(package);

            packagesMockSet.Verify(m => m.Remove(package), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CalculatePackageCost()
        {
            var package = new Package
            {
                Id = 1,
                Width = 10,
                Height = 10,
                Length = 10,
                Weight = 10
            };
            var packages = new List<Package> { package }.AsQueryable();
            var packagesMockSet = MockDbSetHelper.GetMockSet(packages);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            var pricingStrategyMock = new Mock<IPricingStrategy>();
            pricingStrategyMock.Setup(m => m.GetCost(It.IsAny<Package>())).Returns(0.750M);
            var packagesService = new PackageService(mockContext.Object, new RegularPricingStrategy());

            var result = packagesService.CalculatePackageCost(package);
            Assert.Equal(result, 0.750M);
        }
    }
}