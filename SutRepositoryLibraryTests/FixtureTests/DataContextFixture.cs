using Microsoft.EntityFrameworkCore;
using Npgsql;
using SutLibrary.Data;

namespace SutRepositoryLibraryTests.FixtureTests
{
    /// <summary>
    /// An Xunit Test Fixture to provide a PostgreSql Testcontainer to all tests.
    /// This is shared so it is only spun up once and shared among tests.
    /// <para>
    /// Spinning up a separate container for each test resulted in several 'System.TimeoutException : The operation has timed out.'
    /// errors from DockerContainer.StartAsync(CancellationToken ct) in the InitializeAsync() method.</para>
    /// </summary>
    public sealed class DataContextFixture : IAsyncLifetime
    {
        /// <summary>
        /// The PostgreSql database container for the Provider database context
        /// </summary>
        private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
            .WithCleanUp(true)
            .WithImage("postgres:15-alpine")
            //.WithStartupCallback()
            //.WithWaitStrategy(Wait.)
            .Build();

        private DbContextOptions<SutDbContext> _dbContextOptions;

        public SutDbContext Context { get; private set; }

        public Task DisposeAsync()
        {
            return _postgres.DisposeAsync().AsTask();
        }

        public async Task InitializeAsync()
        {
            // It is expected that Class Fixture instance is created once before all tests are run
            // but apparently this InitializeAsync will still be called every test run
            // While the _postgres object might be created only once, the StartAsync of the docker Testcontainer
            // seems to break for some projects.
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
        }
    }
}
