using Moq;
using System.Data.Entity;
using System.Linq;

namespace InstantDelivery.Tests
{
    /// <summary>
    /// Zapewnia pomocnicze metody do mockowania obiektów <see cref="DbSet"/>
    /// </summary>
    public static class MockDbSetHelper
    {
        public static Mock<DbSet<T>> GetMockSet<T>(IQueryable<T> data) where T : class
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
