using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SutLibrary.Data;
using SutLibrary.Entities;

namespace SutLibrary.Services
{
    public partial class SutRepository : ISutRepository, IDisposable
    {
        private readonly SutDbContext _context;
        private readonly ILogger _logger;

        public SutRepository(SutDbContext context, ILogger<ISutRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> AddComplexEntityAsync(ComplexEntity entity, CancellationToken cancellationToken = default)
        {
            _context.ComplexEntities.Add(entity);
            var count = await _context.SaveChangesAsync(cancellationToken);

            return count;
        }

        public async Task<int> AddEntityAsync(TopLevelEntity entity, CancellationToken cancellationToken = default)
        {
            _context.TopLevelEntities.Add(entity);
            var count = await _context.SaveChangesAsync(cancellationToken);

            return count;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<ComplexEntity>> GetComplexEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _context.ComplexEntities.ToListAsync(cancellationToken);

            return entities;
        }

        public async Task<ComplexEntity> GetComplexEntityAsync(string name, string code, string language, int version, CancellationToken cancellationToken = default)
        {
            var entity = await _context.ComplexEntities.FirstOrDefaultAsync(e => e.Name == name && e.Code == code && e.Language == language && e.Version == version, cancellationToken);

            return entity;
        }

        public async Task<List<TopLevelEntity>> GetEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _context.TopLevelEntities.OrderBy(a => a.Id).ToListAsync(cancellationToken);

            return entities;
        }

        public async Task<TopLevelEntity> GetEntityAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.TopLevelEntities.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            return entity;
        }

        public async Task<int> UpdateComplexEntityAsync(ComplexEntity entity, CancellationToken cancellationToken = default)
        {
            _context.ComplexEntities.Update(entity);
            var count = await _context.SaveChangesAsync(cancellationToken);

            return count;
        }

        public async Task<int> UpdateEntityAsync(TopLevelEntity entity, CancellationToken cancellationToken = default)
        {
            _context.TopLevelEntities.Update(entity);
            var count = await _context.SaveChangesAsync(cancellationToken);

            return count;
        }
    }
}
