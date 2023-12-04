using SutLibrary.Entities;
using SutRepositoryLibraryTests.FixtureTests;

namespace SutRepositoryLibraryTests.CollectionFixtureTests
{
    public partial class CollectionFixtureSutRepositoryTests
    {
        public class CollectionFixtureEntitiesTests
        {
            public class CollectionFixtureEntitiesTestBase : CollectionFixtureSutRepositoryTestBase 
            {
                public CollectionFixtureEntitiesTestBase(DataContextCollectionFixture fixture) : base(fixture)
                {
                }
            }

            [Collection("Database collection")]
            public class AddEntityAsyncMethod : CollectionFixtureEntitiesTestBase
            {
                public int Result;

                public AddEntityAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    Result = await Repository.AddEntityAsync(TopLevelEntity1);

                    Assert.Equal(1, Result);
                    Assert.Equal(RecordId1, TopLevelEntity1.Id);
                }
            }

            [Collection("Database collection")]
            public class GetEntitiesAsyncMethod : CollectionFixtureEntitiesTestBase
            {
                public List<TopLevelEntity> Result;

                public GetEntitiesAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    TopLevelEntity1.Id = EntityId42;

                    // This unit test should be the only time the Context DbSet is accessed directly
                    Context.TopLevelEntities.Add(TopLevelEntity1);
                    await Context.SaveChangesAsync();

                    Result = await Repository.GetEntitiesAsync();

                    Assert.NotNull(Result);
                    Assert.NotEmpty(Result);
                    Assert.Single(Result);
                    Assert.Equal(EntityId42, Result[0].Id);
                }
            }

            [Collection("Database collection")]
            public class GetEntityAsyncMethod : CollectionFixtureEntitiesTestBase
            {
                public TopLevelEntity Result;

                public GetEntityAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    TopLevelEntity1.Id = EntityId42;

                    // This unit test should be the only time the Context DbSet is accessed directly
                    Context.TopLevelEntities.Add(TopLevelEntity1);
                    await Context.SaveChangesAsync();

                    Result = await Repository.GetEntityAsync(EntityId42);

                    Assert.NotNull(Result);
                    Assert.Equal(EntityId42, Result.Id);
                }
            }

            [Collection("Database collection")]
            public class UpdateEntityAsyncMethod : CollectionFixtureEntitiesTestBase
            {
                public int Result;

                public UpdateEntityAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    // This unit test should be the only time the Context DbSet is accessed directly
                    Context.TopLevelEntities.Add(TopLevelEntity1);
                    await Context.SaveChangesAsync();

                    TopLevelEntity1.Name = Name2;

                    Result = await Repository.UpdateEntityAsync(TopLevelEntity1);

                    Assert.Equal(1, Result);
                }
            }
        }
    }
}
