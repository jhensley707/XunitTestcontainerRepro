using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Npgsql;
using SutLibrary.Data;
using SutLibrary.Entities;
using SutLibrary.Services;

namespace SutRepositoryLibraryTests.SharedTests
{
    public partial class SharedSutRepositoryTests
    {
        public class SharedSutRepositoryTestBase : CommonTestBase, IAsyncLifetime
        {
            /// <summary>
            /// The PostgreSql database container for the Provider database context
            /// </summary>
            private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
                .WithCleanUp(true)
                .WithImage("postgres:15-alpine")
                .Build();

            private DbContextOptions<SutDbContext> _dbContextOptions;

            public SutDbContext Context;
            public readonly Mock<ILogger<ISutRepository>> MockLogger;
            public SutRepository Repository;

            public SharedSutRepositoryTestBase()
            {
                MockLogger = new Mock<ILogger<ISutRepository>>(MockBehavior.Strict);
                MockLogger.Setup(m => m.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
            }

            public Task DisposeAsync()
            {
                return _postgres.DisposeAsync().AsTask();
            }

            public async Task InitializeAsync()
            {
                // It is expected that an class instance is created for each test so this InitializeAsync will be called every test run
                await _postgres.StartAsync();

                var connectionString = _postgres.GetConnectionString();
                _dbContextOptions = new DbContextOptionsBuilder<SutDbContext>()
                    .UseNpgsql(connectionString)
                    .Options;
                Context = new SutDbContext(_dbContextOptions);

                await Context.Database.MigrateAsync();

                // Close and reopen the connection to reload types
                NpgsqlConnection connection = (NpgsqlConnection)Context.Database.GetDbConnection();
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.ReloadTypes();

                Repository = new SutRepository(Context, MockLogger.Object);
            }
        }
    }
}