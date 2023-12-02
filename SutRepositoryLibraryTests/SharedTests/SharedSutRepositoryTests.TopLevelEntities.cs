using SutLibrary.Entities;

namespace SutRepositoryLibraryTests
{
    public partial class SharedSutRepositoryTests
    {
        public class SharedTopLevelEntitiesTests
        {
            public class SharedTopLevelEntitiesTestBase : SharedSutRepositoryTestBase 
            {
                public SharedTopLevelEntitiesTestBase() { }
            }

            public class AddEntityAsyncMethod : SharedTopLevelEntitiesTestBase
            {
                public int Result;

                [Fact]
                public async void ShouldReturnResult()
                {
                    Result = await Repository.AddEntityAsync(TopLevelEntity1);

                    // Adding the tenant adds the TenantView record, the TenantApplication record, the AppStatus record and the TenantBilling record
                    Assert.Equal(1, Result);
                    Assert.Equal(RecordId1, TopLevelEntity1.Id);
                }
            }

            public class GetEntitiesAsyncMethod : SharedTopLevelEntitiesTestBase
            {
                public List<TopLevelEntity> Result;

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

            public class GetEntityAsyncMethod : SharedTopLevelEntitiesTestBase
            {
                public TopLevelEntity Result;

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

            public class UpdateEntityAsyncMethod : SharedTopLevelEntitiesTestBase
            {
                public int Result;

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
