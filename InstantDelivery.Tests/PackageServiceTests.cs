using AutoMapper;
using InstantDelivery.Common.Enums;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model.Employees;
using InstantDelivery.Model.Packages;
using InstantDelivery.Service;
using InstantDelivery.Service.Controllers;
using InstantDelivery.Service.Pricing;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Results;
using Xunit;

namespace InstantDelivery.Tests
{

    public class PackageServiceTests
    {

        public PackageServiceTests()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [Fact]
        public void RegisterPackage_AddsPackageToDatabase()
        {
            var mockSet = MockDbSetHelper.CreateMockSet(new Package[] { }.AsQueryable());
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(m => m.Packages).Returns(mockSet.Object);
            mockContext.Setup(m => m.PackageEvents).ReturnsDbSet(new List<PackageEvent>());
            var pricingMock = new Mock<IPricingStrategy>();
            var package = new Package
            {
                Width = 100,
                Height = 200,
                Length = 150,
                Weight = 100
            };

            var controller = new PackagesController(mockContext.Object, pricingMock.Object);
            var packageDto = new PackageDto();

            Mapper.DynamicMap(package, packageDto, typeof(Package), typeof(PackageDto));
            controller.RegisterPackage(packageDto);

            mockSet.Verify(m => m.Add(It.IsAny<Package>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void RegisterPackage_SetsStatusToNew()
        {
            var mockContext = GetEmptyMockContext();
            mockContext.Setup(m => m.PackageEvents).ReturnsDbSet(new List<PackageEvent>());
            var mockPricingStrategy = new Mock<IPricingStrategy>();
            var controller = new PackagesController(mockContext.Object, mockPricingStrategy.Object);

            var package = new Package();
            var packageDto = new PackageDto();
            Mapper.DynamicMap(package, packageDto, typeof(Package), typeof(PackageDto));

            controller.RegisterPackage(packageDto);

            Assert.Equal(PackageStatus.New, package.Status);
        }

        [Fact]
        public void RegisterPackage_AssignsCostToThePackage()
        {
            var mockSet = new Mock<IDbSet<Package>>();
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(m => m.PackageEvents).ReturnsDbSet(new List<PackageEvent>());
            mockContext.Setup(m => m.Packages).Returns(mockSet.Object);
            var packageDto = new PackageDto();

            var mockPricingStrategy = new Mock<IPricingStrategy>();
            mockPricingStrategy.Setup(m => m.GetCost(packageDto)).Returns(10M);

            var controller = new PackagesController(mockContext.Object, mockPricingStrategy.Object);
            controller.RegisterPackage(packageDto);

            mockSet.Verify(m => m.Add(It.Is((Package p) => p.Cost == 10M)), Times.Once());
        }

        [Fact]
        public void RegisterPackage_ShouldAddEventToPackageHistory()
        {
            var mockSet = new Mock<IDbSet<PackageEvent>>();
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(new List<Package>());
            mockContext.Setup(c => c.PackageEvents).Returns(mockSet.Object);
            var packageDto = new PackageDto { Id = 1 };

            var mockPricingStrategy = new Mock<IPricingStrategy>();
            var controller = new PackagesController(mockContext.Object, mockPricingStrategy.Object);

            controller.RegisterPackage(packageDto);

            mockSet.Verify(m => m.Add(It.Is((PackageEvent p) => p.Package.Id == 1 &&
                                                                p.EventType == PackageEventType.Registered)), Times.Once());
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
            var employee = new Employee { Id = 1, FirstName = "A", LastName = "B" };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);
            mockContext.Setup(m => m.PackageEvents).ReturnsDbSet(new List<PackageEvent>());

            var controller = new PackagesController(mockContext.Object, null);

            controller.AssignPackage(package.Id, employee.Id);

            Assert.Equal(PackageStatus.InDelivery, package.Status);
            Assert.Equal(1, employee.Packages.Count);
            Assert.Equal(package, employee.Packages.First());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        [Fact]
        public void AssignPackage_ShouldAddEventToPackageHistory()
        {
            var package = new Package
            {
                Id = 1,
            };
            var employee = new Employee { Id = 1, FirstName = "A", LastName = "B" };

            var mockSet = new Mock<IDbSet<PackageEvent>>();
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);
            mockContext.Setup(c => c.PackageEvents).Returns(mockSet.Object);
            var controller = new PackagesController(mockContext.Object, null);

            controller.AssignPackage(package.Id, employee.Id);

            mockSet.Verify(m => m.Add(It.Is((PackageEvent pe) => pe.Package.Id == package.Id &&
                                                                 pe.Employee.Id == employee.Id &&
                                                                 pe.EventType == PackageEventType.HandedToCourier)));
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
            var packagesMockSet = MockDbSetHelper.CreateMockSet(packages);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            mockContext.Setup(m => m.PackageEvents).ReturnsDbSet(new List<PackageEvent>());
            var pricingStrategy = new Mock<IPricingStrategy>().Object;
            var controller = new PackagesController(mockContext.Object, pricingStrategy);
            var packageDto = new PackageDto();
            Mapper.DynamicMap(package, packageDto, typeof(Package), typeof(PackageDto));
            controller.RegisterPackage(packageDto);

            packagesMockSet.Verify(m => m.Add(It.IsAny<Package>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void GetById_ShouldReturnPackage()
        {
            var package = new Package { Height = 5, Width = 10, Length = 10, Weight = 10, Status = PackageStatus.New, Id = 1 };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);

            var controller = new PackagesController(mockContext.Object, null);

            var packageDto = (controller.Get(package.Id) as OkNegotiatedContentResult<PackageDto>)?.Content;

            Assert.Equal(packageDto?.Id, package.Id);
            Assert.Equal(packageDto?.Status, package.Status);
        }

        [Fact]
        public void RemovePackage_ShouldRemovePackage()
        {
            var package = new Package
            {
                Id = 1,
                Status = PackageStatus.InDelivery
            };
            var packages = new List<Package> { package };
            var packagesMockSet = MockDbSetHelper.CreateMockSet(packages.AsQueryable());

            var employee = new Employee { Id = 1, FirstName = "A", LastName = "B" };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).Returns(packagesMockSet.Object);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);

            var controller = new PackagesController(mockContext.Object, null);
            controller.Delete(package.Id);

            packagesMockSet.Verify(m => m.Remove(It.Is((Package p) => p.Id == package.Id)), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void DetachPackageFromEmployee_ForCorrectConditions_ShouldDetachPackageFromEmployee()
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
            var employee = new Employee { Id = 1, FirstName = "A", LastName = "B" };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);
            mockContext.Setup(m => m.PackageEvents).ReturnsDbSet(new List<PackageEvent>());

            var controller = new PackagesController(mockContext.Object, null);

            controller.AssignPackage(package.Id, employee.Id);

            controller.DetachPackageFromEmployee(package.Id);
            Assert.Equal(0, employee.Packages.Count);
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
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);

            var pricingStrategyMock = new Mock<IPricingStrategy>();
            pricingStrategyMock.Setup(m => m.GetCost(It.IsAny<PackageDto>())).Returns(0.750M);

            var controller = new PackagesController(mockContext.Object, pricingStrategyMock.Object);

            var result = controller.GetPackageCost(Mapper.Map<PackageDto>(package)) as OkNegotiatedContentResult<decimal>;
            Assert.Equal(result?.Content, 0.750M);
        }

        [Fact]
        public void MarkAsDelivered_ShouldSetPackageStatusToDelivered()
        {
            var package = new Package { Id = 1, Status = PackageStatus.InDelivery };
            var employee = new Employee { Id = 1 };
            employee.Packages.Add(package);
            var mockContext = GetEmptyMockContext();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);
            mockContext.Setup(m => m.PackageEvents).ReturnsDbSet(new List<PackageEvent>());

            var controller = new PackagesController(mockContext.Object, null);
            controller.MarkAsDelivered(package.Id);

            Assert.Equal(PackageStatus.Delivered, package.Status);
        }

        [Fact]
        public void MarkAsDelivered_ShouldAddEventToPackageHistory()
        {
            var package = new Package { Id = 1, Status = PackageStatus.InDelivery };
            var employee = new Employee { Id = 1 };
            employee.Packages.Add(package);

            var mockSet = new Mock<IDbSet<PackageEvent>>();
            var mockContext = GetEmptyMockContext();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);
            mockContext.Setup(c => c.PackageEvents).Returns(mockSet.Object);

            var controller = new PackagesController(mockContext.Object, null);
            controller.MarkAsDelivered(package.Id);

            mockSet.Verify(m => m.Add(It.Is((PackageEvent pe) => pe.Package.Id == package.Id &&
                                                                 pe.Employee.Id == employee.Id &&
                                                                 pe.EventType == PackageEventType.Delivered)));
        }

        [Fact]
        public void MarkAsDelivered_ShouldRemovePackageFromEmployeeCollection()
        {
            var package = new Package { Id = 1, Status = PackageStatus.InDelivery };
            var employee = new Employee { Id = 1 };
            employee.Packages.Add(package);
            var mockContext = GetEmptyMockContext();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);
            mockContext.Setup(m => m.PackageEvents).ReturnsDbSet(new List<PackageEvent>());

            var controller = new PackagesController(mockContext.Object, null);
            controller.MarkAsDelivered(package.Id);

            Assert.Equal(employee.Packages.Count, 0);
        }

        [Fact]
        public void GetAssignedEmployee_ShouldReturnAssignedEmployee()
        {
            var package = new Package { Id = 1, Status = PackageStatus.InDelivery };
            var employee = new Employee { Id = 1 };
            employee.Packages.Add(package);
            var mockContext = GetEmptyMockContext();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);
            mockContext.Setup(m => m.PackageEvents).ReturnsDbSet(new List<PackageEvent>());

            var controller = new PackagesController(mockContext.Object, null);
            var response = controller.GetAssignedEmployee(package.Id) as OkNegotiatedContentResult<EmployeeDto>;

            Assert.NotNull(response);
            Assert.Equal(response.Content.Id, employee.Id);
        }

        private static Mock<InstantDeliveryContext> GetEmptyMockContext()
        {
            var mockSet = new Mock<DbSet<Package>>();
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(m => m.Packages).Returns(mockSet.Object);
            return mockContext;
        }
    }
}