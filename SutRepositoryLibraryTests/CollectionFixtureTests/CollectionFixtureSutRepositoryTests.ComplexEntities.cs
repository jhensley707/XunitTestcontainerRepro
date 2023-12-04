using SutLibrary.Entities;

namespace SutRepositoryLibraryTests.CollectionFixtureTests
{
    public partial class CollectionFixtureSutRepositoryTests
    {
        public class CollectionFixtureComplexEntitiesTests
        {
            public class CollectionFixtureComplexEntitiesTestBase : CollectionFixtureSutRepositoryTestBase 
            {
                public CollectionFixtureComplexEntitiesTestBase(DataContextCollectionFixture fixture) : base(fixture) { }
            }

            [Collection("Database collection")]
            public class AddComplexEntityAsyncMethod : CollectionFixtureComplexEntitiesTestBase
            {
                public int Result;

                public AddComplexEntityAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    Result = await Repository.AddComplexEntityAsync(ComplexEntity1);

                    Assert.Equal(1, Result);
                }
            }

            [Collection("Database collection")]
            public class GetComplexEntitiesAsyncMethod : CollectionFixtureComplexEntitiesTestBase
            {
                public List<ComplexEntity> Result;

                public GetComplexEntitiesAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    // This unit test should be the only time the Context DbSet is accessed directly
                    Context.ComplexEntities.Add(ComplexEntity1);
                    await Context.SaveChangesAsync();

                    Result = await Repository.GetComplexEntitiesAsync();

                    Assert.NotNull(Result);
                    Assert.NotEmpty(Result);
                    Assert.Single(Result);
                    Assert.Equal(Name1, Result[0].Name);
                    Assert.Equal(Code1, Result[0].Code);
                    Assert.Equal(Language1, Result[0].Language);
                    Assert.Equal(Version12, Result[0].Version);
                }
            }

            [Collection("Database collection")]
            public class GetComplexEntityAsyncMethod : CollectionFixtureComplexEntitiesTestBase
            {
                public ComplexEntity Result;

                public GetComplexEntityAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    // This unit test should be the only time the Context DbSet is accessed directly
                    Context.ComplexEntities.Add(ComplexEntity1);
                    await Context.SaveChangesAsync();

                    Result = await Repository.GetComplexEntityAsync(Name1, Code1, Language1, Version12);

                    Assert.NotNull(Result);
                    Assert.Equal(Name1, Result.Name);
                    Assert.Equal(Code1, Result.Code);
                    Assert.Equal(Language1, Result.Language);
                    Assert.Equal(Version12, Result.Version);
                }
            }

            [Collection("Database collection")]
            public class UpdateConplexEntityAsyncMethod : CollectionFixtureComplexEntitiesTestBase
            {
                public int Result;

                public UpdateConplexEntityAsyncMethod(DataContextCollectionFixture fixture) : base(fixture) { }

                [Fact]
                public async void ShouldReturnResult()
                {
                    // This unit test should be the only time the Context DbSet is accessed directly
                    Context.ComplexEntities.Add(ComplexEntity1);
                    await Context.SaveChangesAsync();

                    ComplexEntity1.Value = Value1;

                    Result = await Repository.UpdateComplexEntityAsync(ComplexEntity1);

                    Assert.Equal(1, Result);
                }
            }
        }
    }
}
