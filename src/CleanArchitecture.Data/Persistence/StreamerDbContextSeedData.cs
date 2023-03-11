using CleanArchitecture.Domain;

using Microsoft.Extensions.Logging;

using System.Text.Json;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeedData
    {
        public static async Task LoadDataAsync(StreamerDbContext context, ILoggerFactory loggerFactory)
        {
			try
			{
				var videos = new List<Video>();
				if (!context.Directores!.Any())
				{
					var directorData = File.ReadAllText("../CleanArchitecture.Data/Data/director.json");
					var directores = JsonSerializer.Deserialize<List<Director>>(directorData);

					await context.Directores.AddRangeAsync(directores);
					await context.SaveChangesAsync();
				}

                if (!context.Videos!.Any())
                {
                    var videoData = File.ReadAllText("../CleanArchitecture.Data/Data/video.json");
                    videos = JsonSerializer.Deserialize<List<Video>>(videoData);
                    await GetPreconfigureVideoDirectorAsync(videos!, context);
                    //await context.Videos!.AddRangeAsync(videos);
                    await context.SaveChangesAsync();
                }
                if (!context.Actores!.Any())
                {
                    var actorData = File.ReadAllText("../CleanArchitecture.Data/Data/actor.json");
                    var actores = JsonSerializer.Deserialize<List<Actor>>(actorData);
                    await context.Actores!.AddRangeAsync(actores);
                    await context.SaveChangesAsync();
                    
                    GetPreconfigureVideoActorAsync(videos!, actores!, context);
                    await context.SaveChangesAsync();
                }
                
            }
			catch (Exception ex)
			{
                var logger = loggerFactory.CreateLogger<StreamerDbContextSeedData>();
                logger.LogError(ex.Message);
				throw;
			}
        }

        private static async Task GetPreconfigureVideoDirectorAsync(List<Video> videos, StreamerDbContext context)
        {
            var random = new Random();
            foreach (var video in videos)
            {
                video.DirectorId = random.Next(1, 99);
            }

            await context.Videos!.AddRangeAsync(videos);
        }

        private static void GetPreconfigureVideoActorAsync(List<Video> videos, List<Actor> acores, StreamerDbContext context)
        {
            var random = new Random();

            foreach (var video in videos)
            {
                var actor = acores.FirstOrDefault(x => x.Id == random.Next(1, 99));

                if (actor != null)
                {
                    if (video.Atores == null) video.Atores = new List<Actor>();
                        video.Atores!.Add(actor);
                    context.Videos!.Update(video);
                }
            }
        }

    }
}
