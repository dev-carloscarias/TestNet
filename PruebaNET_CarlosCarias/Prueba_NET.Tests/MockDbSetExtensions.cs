using Microsoft.EntityFrameworkCore;
using Moq;
using Prueba_NET.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

public static class MockDbSetExtensions
{
    public static Mock<DbSet<T>> ReturnsDbSet<T>(this Mock<ApplicationDbContext> mockContext, IEnumerable<T> entities) where T : class
    {
        var queryable = entities.AsQueryable();
        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        dbSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(entities.ToList().Add);
        mockContext.Setup(c => c.Set<T>()).Returns(dbSet.Object);
        return dbSet;
    }
}
