using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using Moq;
using System.Data.Entity;
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
            var service = new PackageService(mockContext.Object);
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
            var mockSet = new Mock<DbSet<Package>>();
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(m => m.Packages).Returns(mockSet.Object);
            var service = new PackageService(mockContext.Object);
            var package = new Package()
            {
                Width = 100,
                Height = 200,
                Length = 150,
                Weight = 100
            };

            service.RegisterPackage(package);

            Assert.Equal(PackageStatus.New, package.Status);
        }

    }
}