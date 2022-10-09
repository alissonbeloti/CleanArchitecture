using CleanArchitecture.Domain;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence;

public class StreamerDbContextSeed
{
    public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> looger, CancellationToken cancellationToken)
    {
        if (context.Streamers!.Any())
        {
            context.Streamers!.AddRange(GetPreconfiguredStreamer());
            await context.SaveChangesAsync(cancellationToken);
            looger.LogInformation("Estmaos inserindo novos registros na database {context}", typeof(StreamerDbContext).Name);
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
