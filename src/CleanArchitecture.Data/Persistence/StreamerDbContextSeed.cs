using CleanArchitecture.Domain;

using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence;

public class StreamerDbContextSeed
{
    public static async Task SeedAsync(StreamerDbContext context, ILoggerFactory logerFactory, CancellationToken cancellationToken)
    {
        if (!context.Streamers!.Any())
        {
            var logger = logerFactory.CreateLogger<StreamerDbContextSeed>();
            context.Streamers!.AddRange(GetPreconfiguredStreamer());
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Estamos inserindo novos registros na database {context}", typeof(StreamerDbContext).Name);
        }
    }

    private static IEnumerable<Streamer> GetPreconfiguredStreamer()
    {
        return new List<Streamer>
        {
            new Streamer { CreatedBy = "alisson", Nome = "Maxi HBP", Url = "http://www.hbp.com"},
            new Streamer { CreatedBy = "alisson", Nome = "Amazon Vip", Url = "http://www.amazonvip.com"},
        };
    }
}
