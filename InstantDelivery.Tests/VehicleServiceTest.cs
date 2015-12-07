using AutoMapper;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Model.Vehicles;
using InstantDelivery.Service;
using InstantDelivery.Service.Controllers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Xunit;

namespace InstantDelivery.Tests
{

    public class VehicleServiceTest
    {
        public VehicleServiceTest()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [Fact]
        public void GetAllVehiclesModels_ShouldReturnAllVehiclesModels()
        {
            var vehiclesModels = new List<VehicleModel>
            {
                new VehicleModel {Id = 1},
                new VehicleModel {Id = 2},
                new VehicleModel {Id = 3}
            };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.VehicleModels).ReturnsDbSet(vehiclesModels);
            var controller = new VehiclesController(mockContext.Object);

            var result = controller.GetModels() as OkNegotiatedContentResult<List<VehicleModel>>;
            var count = result?.Content.Count;
            Assert.Equal(count, 3);
        }

        [Fact]
        public void RemoveVehicle_ShouldRemoveVehicle()
        {
            var vehicles = new List<Vehicle> { new Vehicle
            {
                Id=1,
                RegistrationNumber="1"
            } }.AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.CreateMockSet(vehicles);
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);

            var controller = new VehiclesController(mockContext.Object);
            controller.Delete(vehicles.First().Id);

            vehiclesMockSet.Verify(m => m.Remove(vehicles.First()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void AddVehicle_ShouldAlwaysAddVehicle()
        {
            var vehicleModel = new VehicleModel { Id = 1 };
            var vehicles = new List<Vehicle>().AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.CreateMockSet(vehicles);
            var vehicleToAdd = new AddVehicleDto { Id = 1, VehicleModelId = 1, RegistrationNumber = "" };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);
            mockContext.Setup(c => c.VehicleModels).ReturnsDbSet(vehicleModel);

            var controller = new VehiclesController(mockContext.Object);
            controller.Post(vehicleToAdd);

            vehiclesMockSet.Verify(m => m.Add(It.Is((Vehicle v) => v.Id == vehicleToAdd.Id)), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Put_UpdatesVehicleProperties()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle {Id=1, RegistrationNumber="1"},
            }.AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.CreateMockSet(vehicles);
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);

            var controller = new VehiclesController(mockContext.Object);
            var selected = vehiclesMockSet.Object.FirstOrDefault();

            vehiclesMockSet.Object.Attach(selected);
            selected.RegistrationNumber = "2";
            var vehicleDto = Mapper.Map<VehicleDto>(selected);

            controller.Put(vehicleDto);

            var result = vehiclesMockSet.Object.FirstOrDefault();
            Assert.Equal(result?.RegistrationNumber, "2");
        }

        [Fact]
        public void GetAllAvailable_ForSpecifiedVehicle()
        {
            var vehicleModel = new VehicleModel { Id = 1 };
            var vehicles = new List<Vehicle>
            {
                new Vehicle {Id = 1, RegistrationNumber = "1", VehicleModel = vehicleModel},
                new Vehicle {Id = 2, RegistrationNumber = "2", VehicleModel = vehicleModel},
                new Vehicle {Id = 3, RegistrationNumber = "3", VehicleModel = vehicleModel},
            };
            var employees = new List<Employee>
            {
                new Employee {FirstName = "J.D", LastName = "Kyle", Vehicle = vehicles[1]},
                new Employee {FirstName = "Ted", LastName = "Mosby", Vehicle = vehicles[0]},
            };
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Employees).ReturnsDbSet(employees);
            mockContext.Setup(c => c.Vehicles).ReturnsDbSet(vehicles);
            mockContext.Setup(c => c.VehicleModels).ReturnsDbSet(vehicleModel);

            var controller = new VehiclesController(mockContext.Object);

            var result = controller.GetAllAvailable(new PageQuery { PageSize = 10, PageIndex = 1 })
                as OkNegotiatedContentResult<PagedResult<VehicleDto>>;

            Assert.Equal(result?.Content.PageCollection.Count, 1);
        }
    }
}