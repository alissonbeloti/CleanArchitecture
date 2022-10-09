using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public class VideoRepository : RepositoryBase<Video>, IVideoRepository
{
    public VideoRepository(StreamerDbContext context) : base(context)
    {
    }

    public async Task<Video> GetByTitle(string title)
    {
        return await _context.Videos!.Where(v => v.Nome == title).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Video>> GetByUserName(string username)
    {
        return await _context.Videos!.Where(v => v.CreatedBy == username).ToListAsync();
    }
}
