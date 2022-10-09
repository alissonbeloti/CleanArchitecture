using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IUnityOfWork: IDisposable
    {
        public IVideoRepository VideoRepository { get; }
        public IStreamerRepository StreamerRepository { get; }

        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel, new();


        Task<int> Complete();
    }
}
