using Microsoft.Extensions.Logging;
using Moq;
using SutLibrary.Data;
using SutLibrary.Entities;
using SutLibrary.Services;

namespace SutRepositoryLibraryTests.CollectionFixtureTests
{
    public partial class CollectionFixtureSutRepositoryTests
    {
        public class CollectionFixtureSutRepositoryTestBase : CommonTestBase
        {
            public DataContextCollectionFixture Fixture { get; set; }
            public SutDbContext Context;
            public readonly Mock<ILogger<ISutRepository>> MockLogger;
            public SutRepository Repository;

            public CollectionFixtureSutRepositoryTestBase(DataContextCollectionFixture fixture)
            {
                Fixture = fixture;
                Context = fixture.Context;
                MockLogger = new Mock<ILogger<ISutRepository>>(MockBehavior.Strict);
                MockLogger.Setup(m => m.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
                Repository = new SutRepository(Fixture.Context, MockLogger.Object);

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
        }
    }
}