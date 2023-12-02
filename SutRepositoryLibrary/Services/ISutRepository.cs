using SutLibrary.Entities;

namespace SutLibrary.Services
{
    public partial interface ISutRepository : IDisposable
    {
        Task<int> AddComplexEntityAsync(ComplexEntity entity, CancellationToken cancellationToken = default);
        Task<int> AddEntityAsync(TopLevelEntity entity, CancellationToken cancellationToken = default);

        Task<List<ComplexEntity>> GetComplexEntitiesAsync(CancellationToken cancellationToken = default);
        Task<ComplexEntity> GetComplexEntityAsync(string name, string code, string language, int version, CancellationToken cancellationToken = default);
        Task<List<TopLevelEntity>> GetEntitiesAsync(CancellationToken cancellationToken = default);
        Task<TopLevelEntity> GetEntityAsync(int id, CancellationToken cancellationToken = default);

        Task<int> UpdateComplexEntityAsync(ComplexEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateEntityAsync(TopLevelEntity entity, CancellationToken cancellationToken = default);
    }
}
