using InstantDelivery.Domain.Entities;
using Moq;
using Moq.Language;
using Moq.Language.Flow;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InstantDelivery.Tests
{
    /// <summary>
    /// Zapewnia pomocnicze metody do mockowania obiektów <see cref="DbSet"/>
    /// </summary>
    public static class MockDbSetHelper
    {
        public static Mock<DbSet<T>> CreateMockSet<T>(IQueryable<T> data)
            where T : Entity
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
                   .Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression)
                   .Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType)
                   .Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
                   .Returns(queryableData.GetEnumerator());
            mockSet.As<IDbSet<T>>().Setup(m => m.Find(It.IsAny<object[]>()))
                   .Returns((object[] id) => data.FirstOrDefault(e => e.Id == (int)id[0]));
            mockSet.Setup(m => m.Include(It.IsAny<string>()))
                   .Returns(mockSet.Object);
            return mockSet;
        }

        public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
                this IReturns<TContext, IDbSet<TEntity>> setup,
                IQueryable<TEntity> entities)
            where TEntity : Entity
            where TContext : DbContext
        {
            var mockSet = CreateMockSet(entities);
            return setup.Returns(mockSet.Object);
        }

        public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
                this IReturns<TContext, IDbSet<TEntity>> setup,
                IEnumerable<TEntity> entities)
            where TEntity : Entity
            where TContext : DbContext
        {
            var mockSet = CreateMockSet(entities.AsQueryable());
            return setup.Returns(mockSet.Object);
        }

        public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
        this IReturns<TContext, IDbSet<TEntity>> setup,
        TEntity entity)
            where TEntity : Entity
            where TContext : DbContext
        {
            var mockSet = CreateMockSet(new[] { entity }.AsQueryable());
            return setup.Returns(mockSet.Object);
        }
    }
}
