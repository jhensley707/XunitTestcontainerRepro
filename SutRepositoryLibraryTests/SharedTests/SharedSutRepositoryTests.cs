using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Npgsql;
using SutLibrary.Data;
using SutLibrary.Entities;
using SutLibrary.Services;

namespace SutRepositoryLibraryTests
{
    public partial class SharedSutRepositoryTests
    {
        public class SharedSutRepositoryTestBase : IAsyncLifetime
        {
            public const string Code1 = "Code1";
            public const string Code2 = "Code2";
            public const int EntityId42 = 42;
            public const int EntityId43 = 43;
            public const string Language1 = "Language1";
            public const string Name1 = "Name1";
            public const string Name2 = "Name2";
            public const int RecordId1 = 1;
            public const string Value1 = "Value1";
            public const int Version12 = 12;

            /// <summary>
            /// The PostgreSql database container for the Provider database context
            /// </summary>
            private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
                .WithCleanUp(true)
                .WithImage("postgres:15-alpine")
                .Build();

            private DbContextOptions<SutDbContext> _dbContextOptions;

            public ComplexEntity ComplexEntity1;
            public TopLevelEntity TopLevelEntity1;

            public SutDbContext Context;
            public readonly Mock<ILogger<ISutRepository>> MockLogger;
            public SutRepository Repository;

            public SharedSutRepositoryTestBase()
            {
                MockLogger = new Mock<ILogger<ISutRepository>>(MockBehavior.Strict);
                MockLogger.Setup(m => m.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));

                ComplexEntity1 = new ComplexEntity
                {
                    Code = Code1,
                    Name = Name1,
                    Language = Language1,
                    Version = Version12,
                };
                TopLevelEntity1 = new TopLevelEntity
                {
                    Name = Name1,
                };
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