namespace SutLibrary.Entities
{
    public class TopLevelEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// This complex object type requires PostgreSql hstore extension which is manually added to migration script
        /// </summary>
        public Dictionary<string, string> Settings { get; set; }
    }
}
