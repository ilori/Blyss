namespace Blyss.Tests.Mocks
{

    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public static class MockDbContext
    {

        public static BlyssContext GetContext()
        {
            DbContextOptions<DbContext> options = new DbContextOptionsBuilder<DbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new BlyssContext(options);
        }

    }

}