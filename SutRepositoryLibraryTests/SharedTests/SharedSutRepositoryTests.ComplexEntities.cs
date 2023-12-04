using SutLibrary.Entities;

namespace SutRepositoryLibraryTests.SharedTests
{
    public partial class SharedSutRepositoryTests
    {
        public class SharedComplexEntitiesTests
        {
            public class SharedComplexEntitiesTestBase : SharedSutRepositoryTestBase 
            {
                public SharedComplexEntitiesTestBase() { }

                public class AddComplexEntityAsyncMethod : SharedComplexEntitiesTestBase
                {
                    public int Result;

                    [Fact]
                    public async void ShouldReturnResult()
                    {
                        Result = await Repository.AddComplexEntityAsync(ComplexEntity1);

                        Assert.Equal(1, Result);
                    }
                }

                public class GetComplexEntitiesAsyncMethod : SharedComplexEntitiesTestBase
                {
                    public List<ComplexEntity> Result;

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

                public class GetComplexEntityAsyncMethod : SharedComplexEntitiesTestBase
                {
                    public ComplexEntity Result;

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

                public class UpdateConplexEntityAsyncMethod : SharedComplexEntitiesTestBase
                {
                    public int Result;

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
}
