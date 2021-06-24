using Entities;
using Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Entity.Service.Tests.PersonServiceTests
{
    public abstract class PersonServiceTestBase
    {
        protected readonly IEntityService<Person> EntityService;
        protected readonly DateTime UtcNow = DateTime.UtcNow;
        protected readonly EfStoredProcedureDbContext Context;
        private readonly DbContextOptions<EfStoredProcedureDbContext> _options;

        public PersonServiceTestBase()
        {
            _options = new DbContextOptionsBuilder<EfStoredProcedureDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: $"TESTING_{this.GetType().Name}_{DateTime.Now.Ticks}")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var services = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            Context = new EfStoredProcedureDbContext(_options);
            
            EntityService = new PersonService(new EfStoredProcedureDbContext(_options), services.GetService<ILogger<Person>>());
        }
    }
}
