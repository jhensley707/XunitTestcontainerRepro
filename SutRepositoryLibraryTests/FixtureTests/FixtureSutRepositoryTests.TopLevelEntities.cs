using SutLibrary.Entities;

namespace SutRepositoryLibraryTests.FixtureTests
{
    public partial class FixtureSutRepositoryTests
    {
        public class FixtureEntitiesTests
        {
            public class FixtureEntitiesTestBase : FixtureSutRepositoryTestBase 
            {
                public FixtureEntitiesTestBase(DataContextFixture fixture) : base(fixture)
                {
                }
            }
            public class AddEntityAsyncMethod : FixtureEntitiesTestBase
            {
                public int Result;

                public AddEntityAsyncMethod(DataContextFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    Result = await Repository.AddEntityAsync(TopLevelEntity1);

                    Assert.Equal(1, Result);
                    Assert.Equal(RecordId1, TopLevelEntity1.Id);
                }
            }

            public class GetEntitiesAsyncMethod : FixtureEntitiesTestBase
            {
                public List<TopLevelEntity> Result;

                public GetEntitiesAsyncMethod(DataContextFixture fixture) : base(fixture) { }

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

            public class GetEntityAsyncMethod : FixtureEntitiesTestBase
            {
                public TopLevelEntity Result;

                public GetEntityAsyncMethod(DataContextFixture fixture) : base(fixture) { }

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

            public class UpdateEntityAsyncMethod : FixtureEntitiesTestBase
            {
                public int Result;

                public UpdateEntityAsyncMethod(DataContextFixture fixture) : base(fixture) { }

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
