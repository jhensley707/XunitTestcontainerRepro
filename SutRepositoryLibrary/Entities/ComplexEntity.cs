namespace SutLibrary.Entities
{
    /// <summary>
    /// Specify complex primary key in DbContext
    /// </summary>
    public class ComplexEntity
    {
        public string Code { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Version { get; set; }
    }
}
