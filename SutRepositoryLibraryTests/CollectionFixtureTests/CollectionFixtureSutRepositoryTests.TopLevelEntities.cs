using SutLibrary.Entities;
using SutRepositoryLibraryTests.FixtureTests;
using System.Security.Cryptography.X509Certificates;

namespace SutRepositoryLibraryTests.CollectionFixtureTests
{
    public partial class CollectionFixtureSutRepositoryTests
    {
        public class CollectionFixtureEntitiesTests
        {
            public class CollectionFixtureEntitiesTestBase : CollectionFixtureSutRepositoryTestBase 
            {
                public TopLevelEntity TopLevelEntity2;
                
                public CollectionFixtureEntitiesTestBase(DataContextCollectionFixture fixture) : base(fixture)
                {
                    TopLevelEntity2 = new TopLevelEntity
                    {
                        Name = Name2,
                    };
                }
            }

            [Collection(nameof(DatabaseCollection))]
            public class AddEntityAsyncMethod : CollectionFixtureEntitiesTestBase
            {
                public int Result;

                public AddEntityAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    Result = await Repository.AddEntityAsync(TopLevelEntity2);

                    Assert.Equal(1, Result);
                    Assert.Equal(RecordId2, TopLevelEntity2.Id);
                }
            }

            [Collection(nameof(DatabaseCollection))]
            public class GetEntitiesAsyncMethod : CollectionFixtureEntitiesTestBase
            {
                public List<TopLevelEntity> Result;

                public GetEntitiesAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    Result = await Repository.GetEntitiesAsync();

                    Assert.NotNull(Result);
                    Assert.NotEmpty(Result);
                    Assert.True(Result.Count > 0);
                    Assert.Equal(RecordId1, Result[0].Id);
                }
            }

            [Collection(nameof(DatabaseCollection))]
            public class GetEntityAsyncMethod : CollectionFixtureEntitiesTestBase
            {
                public TopLevelEntity Result;

                public GetEntityAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    Result = await Repository.GetEntityAsync(RecordId1);

                    Assert.NotNull(Result);
                    Assert.Equal(RecordId1, Result.Id);
                }
            }

            [Collection(nameof(DatabaseCollection))]
            public class UpdateEntityAsyncMethod : CollectionFixtureEntitiesTestBase
            {
                public int Result;

                public UpdateEntityAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    TopLevelEntity1.Name = Name2;

                    Result = await Repository.UpdateEntityAsync(TopLevelEntity1);

                    Assert.Equal(1, Result);
                }
            }
        }
    }
}
