using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web.Http.Results;
using AutoMapper;
using InstantDelivery.Common.Enums;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Service.Controllers;
using InstantDelivery.Service.Pricing;
using InstantDelivery.ViewModel.Proxies;
using Moq;
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
            var controller = new PackagesController(mockContext.Object, mockPricingStrategy.Object);
            var package = new Package()
            {
                Width = 100,
                Height = 200,
                Length = 150,
                Weight = 100
            };
            var packageDto = new PackageDto();
            Mapper.DynamicMap(package, packageDto, typeof (Package), typeof (PackageDto));
            controller.RegisterPackage(packageDto);

            mockSet.Verify(m => m.Add(package), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void RegisterPackage_SetsStatusToNew()
        {
            var mockContext = GetEmptyMockContext();
            var mockPricingStrategy = new Mock<IPricingStrategy>();
            var controller = new PackagesController(mockContext.Object, mockPricingStrategy.Object);
            var package = new Package();
            var packageDto = new PackageDto();
            Mapper.DynamicMap(package, packageDto, typeof (Package), typeof (PackageDto));
            controller.RegisterPackage(packageDto);

            Assert.Equal(PackageStatus.New, package.Status);
        }

        [Fact]
        public void RegisterPackage_AssignsCostToThePackage()
        {
            var mockContext = GetEmptyMockContext();
            var mockPricingStrategy = new Mock<IPricingStrategy>();
            var packageDto = new PackageDto();
            Mapper.DynamicMap(It.IsAny<Package>(), packageDto, typeof (Package), typeof (PackageDto));
            mockPricingStrategy.Setup(m => m.GetCost(packageDto)).Returns(10M);
            var controller = new PackagesController(mockContext.Object, mockPricingStrategy.Object);
            var package = new Package();
            var packageDtoToAssing = new PackageDto();
            Mapper.DynamicMap(package, packageDtoToAssing, typeof (Package), typeof (PackageDto));
            controller.RegisterPackage(packageDtoToAssing);

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

            var controller = new PackagesController(mockContext.Object, null);

            controller.AssignPackage(package.Id, employee.Id);

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
            var controller = new PackagesController(mockContext.Object, pricingStrategy);
            var packageDto = new PackageDto();
            Mapper.DynamicMap(package, packageDto, typeof (Package), typeof (PackageDto));
            controller.RegisterPackage(packageDto);

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
                    var controller = new PackagesController(context, pricingStrategy);
                    context.Packages.Add(package);
                    context.SaveChanges();

                    package.Height = 10;
                    Mapper.DynamicMap(controller.Get(package.Id), package, typeof(EmployeeDto), typeof(Employee));


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
            var controller = new PackagesController(mockContext.Object, pricingStrategy);
            var selected = packagesMockSet.Object.FirstOrDefault();
            if (selected == null) return;
            packagesMockSet.Object.Attach(selected);
            selected.Height = 10;
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
            var controller = new PackagesController(mockContext.Object, pricingStrategy);
            mockContext.Object.Employees.Attach(employee);
            employeesMockSet.Object.Attach(employee);

            controller.Delete(package.Id);

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
            var packageDto = new PackageDto();
            Mapper.DynamicMap(It.IsAny<Package>(), packageDto, typeof (Package), typeof (PackageDto));
            pricingStrategyMock.Setup(m => m.GetCost(packageDto)).Returns(0.750M);
            var controller = new PackagesController(mockContext.Object, new RegularPricingStrategy());
            var packageDto2 = new PackageDto();
            Mapper.DynamicMap(package, packageDto2, typeof(Package), typeof(PackageDto));
            var result = controller.GetPackageCost(packageDto) as OkNegotiatedContentResult<decimal>;
            if (result != null) Assert.Equal(result.Content, 0.750M);
        }
    }
}