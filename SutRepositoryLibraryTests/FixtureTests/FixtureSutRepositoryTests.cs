using Microsoft.Extensions.Logging;
using Moq;
using SutLibrary.Data;
using SutLibrary.Entities;
using SutLibrary.Services;
using SutRepositoryLibraryTests.FixtureTests;

namespace SutRepositoryLibraryTests
{
    public partial class FixtureSutRepositoryTests
    {
        public class FixtureSutRepositoryTestBase : IClassFixture<DataContextFixture>
        {
            public const string Code1 = "Code1";
            public const string Code2 = "Code2";
            public const int EntityId42 = 42;
            public const int EntityId43 = 43;
            public const string Language1 = "Language1";
            public const string Name1 = "Name1";
            public const string Name2 = "Name2";
            public const string Name3 = "Name3";
            public const int RecordId1 = 1;
            public const string Value1 = "Value1";
            public const int Version12 = 12;

            public ComplexEntity ComplexEntity1;
            public TopLevelEntity TopLevelEntity1;

            public DataContextFixture Fixture { get; set; }
            public SutDbContext Context;
            public readonly Mock<ILogger<ISutRepository>> MockLogger;
            public SutRepository Repository;

            public FixtureSutRepositoryTestBase(DataContextFixture fixture)
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