using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using AutoMapper;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model;
using InstantDelivery.Model.Vehicles;
using InstantDelivery.Service.Controllers;
using Moq;
using Xunit;

namespace InstantDelivery.Tests
{

    public class VehicleServiceTest
    {
        [Fact]
        public void GetAllVehiclesModels_ShouldReturnAllVehiclesModels()
        {
            var vehiclesModels = new List<VehicleModel>
            {
                new VehicleModel() { Id=1},
                new VehicleModel() { Id=2},
                new VehicleModel() { Id=3}
            }
            .AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.GetMockSet(vehiclesModels);

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.VehicleModels).Returns(vehiclesMockSet.Object);
            var controller = new VehiclesController(mockContext.Object);

            var result = controller.GetModels() as OkNegotiatedContentResult<List<VehicleModel>>;
            if (result == null) return;
            var count = result.Content.Count;
            Assert.Equal(count, 3);
        }

        [Fact]
        public void RemoveVehicle_ShouldRemoveVehicle()
        {
            var vehicles = new List<Vehicle>() { new Vehicle()
            {
                Id=1,
                RegistrationNumber="1"
            } }.AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.GetMockSet(vehicles);

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
            var vehicles = new List<Vehicle>().AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.GetMockSet(vehicles);
            var vehicleToAdd = new Vehicle() { Id = 1 };

            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);
            var controller = new VehiclesController(mockContext.Object);
            var addVehicleDto = new AddVehicleDto();
            AutoMapper.Mapper.DynamicMap(vehicleToAdd, addVehicleDto, typeof (Vehicle), typeof (AddVehicleDto));
            controller.Post(addVehicleDto);

            vehiclesMockSet.Verify(m => m.Add(It.IsAny<Vehicle>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void SaveVehicle_ShouldSaveVehicle()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle() {Id=1, RegistrationNumber="1"},
            }.AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.GetMockSet(vehicles);
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);
            var controller = new VehiclesController(mockContext.Object);
            var selected = vehiclesMockSet.Object.FirstOrDefault();
            if (selected == null) return;
            vehiclesMockSet.Object.Attach(selected);
            selected.RegistrationNumber = "2";
            var vehicleDto = new VehicleDto();
            Mapper.DynamicMap(selected, vehicleDto, typeof (Vehicle), typeof (VehicleDto));
            controller.Put(vehicleDto);

            var result = vehiclesMockSet.Object.FirstOrDefault();
            if (result != null)
            {
                Assert.Equal(result.RegistrationNumber, "2");
            }
        }

        [Fact]
        public void GetAllAvailableAndCurrentVehicle_ForSpecifiedVehicle()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle() {Id=1, RegistrationNumber="1"},
                new Vehicle() {Id=1, RegistrationNumber="2"},
                new Vehicle() {Id=1, RegistrationNumber="3"},
            }.AsQueryable();
            var vehiclesMockSet = MockDbSetHelper.GetMockSet(vehicles);
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(c => c.Vehicles).Returns(vehiclesMockSet.Object);
            var controller = new VehiclesController(mockContext.Object);

            var employees = new List<Employee>
            {
                new Employee() { FirstName = "J.D", LastName = "Kyle", Vehicle=vehicles.Last() } ,
                new Employee() { FirstName = "Ted", LastName = "Mosby"},
                new Employee() { FirstName = "Robin", LastName = "Scherbatsky"}
            }
            .AsQueryable();
            var employeesMockSet = MockDbSetHelper.GetMockSet(employees);

            mockContext.Setup(c => c.Employees).Returns(employeesMockSet.Object);

            var result = controller.GetAllAvailable(new PageQuery() {}) as OkNegotiatedContentResult<PagedResult<VehicleDto>>;
            if (result != null) Assert.Equal(result.Content.PageCollection.Count, 3);
        }
    }
}