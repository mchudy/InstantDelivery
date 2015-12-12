using InstantDelivery.Common.Enums;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model.Employees;
using InstantDelivery.Service;
using InstantDelivery.Service.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using System.Web.Http.Results;
using Xunit;

namespace InstantDelivery.Tests
{
    public class EmployeeServiceTests
    {
        public EmployeeServiceTests()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [Fact]
        public void RemoveEmployee_ForDeliveredPackage_ShouldNotChangeItsStatus()
        {
            var package = new Package
            {
                Id = 1,
                Status = PackageStatus.Delivered
            };
            var employee = new Employee { Id = 1, FirstName = "Ted" };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);
            var userStore = new Mock<UserStore<User>>(mockContext.Object);
            var userManager = new Mock<UserManager<User>>(userStore.Object);
            var controller = new EmployeesController(mockContext.Object, userManager.Object);
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
            var employee = new Employee { Id = 1, FirstName = "Ted", LastName = "Mosby" };
            employee.Packages.Add(package);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Packages).ReturnsDbSet(package);
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);

            var userStore = new Mock<UserStore<User>>(mockContext.Object);
            var userManager = new Mock<UserManager<User>>(userStore.Object);
            var controller = new EmployeesController(mockContext.Object, userManager.Object);
            controller.Delete(employee.Id);

            Assert.Equal(PackageStatus.New, package.Status);
        }

        [Fact]
        public void GetById_ShouldReturnEmployee()
        {
            var employee = new Employee { Id = 1, FirstName = "Ted", LastName = "Mosby" };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);

            var userStore = new Mock<UserStore<User>>(mockContext.Object);
            var userManager = new Mock<UserManager<User>>(userStore.Object);
            var controller = new EmployeesController(mockContext.Object, userManager.Object);
            var result = (controller.Get(employee.Id) as OkNegotiatedContentResult<EmployeeDto>)?.Content;

            Assert.Equal(result?.FirstName, employee.FirstName);
            Assert.Equal(result?.LastName, employee.LastName);
            Assert.Equal(result?.Id, employee.Id);
        }

        [Fact]
        public void ChangeEmployeesVehicle_ShouldChangeEmployeeVehicle()
        {
            var employee = new Employee { FirstName = "Ted", LastName = "Mosby", Id = 1 };
            var vehicle = new Vehicle { Id = 1 };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employee);
            mockContext.Setup(c => c.Vehicles).ReturnsDbSet(vehicle);

            var userStore = new Mock<UserStore<User>>(mockContext.Object);
            var userManager = new Mock<UserManager<User>>(userStore.Object);
            var controller = new EmployeesController(mockContext.Object, userManager.Object);
            controller.ChangeVehicle(employee.Id, vehicle.Id);

            Assert.Equal(employee.Vehicle.Id, 1);
        }
    }
}
