using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;

StreamerDbContext dbContext = new();

//await AddNewStreamerWithVideo();
//await AddNewVideoId();
await AddNewActorWithVideo();


Console.WriteLine("Pressione qualquer tecla para terminar o programa.");
Console.ReadKey();


async Task AddNewActorWithVideo()
{
    var actor = new Actor
    {
        Nome = "Teste",
        Sobrenome = "Sobrenome teste",
    };

    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var video = new Video
    {
      Atores = new List<Actor>() { actor },
      Nome = "Filme teste com diretor obj",
      StreamerId = 2,
      Director = new Director
      {
          Nome = "Diretor",
          Sobrenome = "Teste",
      },
    };
    await dbContext.AddAsync(video);
    await dbContext.SaveChangesAsync();


    //var diretor = new Director
    //{
    //    Nome = "Diretor",
    //    Sobrenome = "Teste",
    //    VideoId = video.Id,
    //};
    //await dbContext.AddAsync(diretor);
    //await dbContext.SaveChangesAsync();
}

async Task AddNewVideoId()
{
    var batmanForever = new Video
    {
        Nome = "Batman Forever",
        StreamerId = 1002,

    };
    await dbContext.AddAsync(batmanForever);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideo()
{
    var streamer = new Streamer
    {
        Nome = "Pantaya",
    };

    var hungerGames = new Video
    {
        Nome = "Hunger Games",
        Streamer = streamer,
        
    };
    await dbContext.AddAsync(hungerGames);
    await dbContext.SaveChangesAsync();
}

//Streamer streamer = new()
//{
//    Nome = "Amazon Prime",
//    Url = "https://www.amazonprime.com"
//};

//dbContext!.Streamers!.Add(streamer);

//await dbContext.SaveChangesAsync();

//var movies = new List<Video> { new Video {
//    Nome = "Mad Max",
//    StreamerId = streamer.Id,
//    },
//    new Video {
//    Nome = "Batman",
//    StreamerId = streamer.Id,
//    },
//    new Video {
//    Nome = "Crepusculo",
//    StreamerId = 1,
//    },
//    new Video {
//    Nome = "Citizen Kane",
//    StreamerId = streamer.Id,
//    },
//};

//await dbContext.AddRangeAsync(movies);
//await dbContext.SaveChangesAsync();

