using System.Collections;

using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Application.Contracts.Persistence;


namespace CleanArchitecture.Infrastructure.Repositories;

public class UnitOfWork : IUnityOfWork 
{ 

    private Hashtable _repositories;
    private readonly StreamerDbContext _context;

    private IVideoRepository _videoRepository;
    private IStreamerRepository _streamerRepository;

    public IVideoRepository VideoRepository => _videoRepository ??= new VideoRepository(_context);
    public IStreamerRepository StreamerRepository => _streamerRepository ??= new StreamerRepository(_context);

    public UnitOfWork(StreamerDbContext context)
    {
        _context = context;
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public async void Dispose()
    {
        await _context.DisposeAsync();
    }

    public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel, new()
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryBase<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(
                typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IAsyncRepository<TEntity>)_repositories[type];
    }
}
