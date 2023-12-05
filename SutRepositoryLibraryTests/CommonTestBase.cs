
using SutLibrary.Entities;

namespace SutRepositoryLibraryTests
{
    /// <summary>
    /// Declared constants and entities to seed the database
    /// </summary>
    public class CommonTestBase
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
        public const int RecordId2 = 2;
        public const string Value1 = "Value1";
        public const int Version12 = 12;

        public ComplexEntity ComplexEntity1;
        public TopLevelEntity TopLevelEntity1;

        public CommonTestBase()
        {
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
