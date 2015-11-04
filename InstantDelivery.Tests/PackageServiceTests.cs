using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using Moq;
using System.Data.Entity;
using Xunit;

namespace InstantDelivery.Tests
{
    public class PackageServiceTests
    {
        private static Mock<InstantDeliveryContext> GetEmptyMockContext()
        {
            var mockSet = new Mock<DbSet<Package>>();
            var mockContext = new Mock<InstantDeliveryContext>();
            mockContext.Setup(m => m.Packages).Returns(mockSet.Object);
            return mockContext;
        }

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

    }
}