using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface IVideoRepository: IAsyncRepository<Video>
{
    Task<Video> GetByTitle(string title);
    Task<IEnumerable<Video>> GetByUserName(string username);
}
