using AutoMapper;

using CleanArchitecture.Domain;
using CleanArchitecture.Application.Features.Videos.Queries.Vms;
using CleanArchitecture.Application.Features.Actors.Queries.Vms;
using CleanArchitecture.Application.Features.Director.Queries.Vms;
using CleanArchitecture.Application.Features.Streamers.Queries.Vms;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Features.Director.Commands.CreateDirector;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

namespace CleanArchitecture.Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Video, VideosVm>().ReverseMap();
        CreateMap<Video, VideosWithIncludesVm>()
            .ForMember(p => p.DirectorNomeCompleto, x => x.MapFrom(
                a => a.Director!.NomeCompleto)
            )
            .ForMember(p => p.StreamerNome, x => x.MapFrom(
                a => a.Streamer!.Nome)
            )
            .ForMember(p => p.Actors, x => x.MapFrom(
                a => a.Atores)
            )
            .ReverseMap();
        CreateMap<Actor, ActorVm>().ReverseMap();
        CreateMap<Director, DirectorVm>().ReverseMap();
        CreateMap<Streamer, StreamerVm>().ReverseMap();
        CreateMap<CreateStreamerCommand, Streamer>().ReverseMap();
        CreateMap<UpdateStreamerCommand, Streamer>().ReverseMap();
        CreateMap<CreateDirectorCommand, Director>().ReverseMap();

    }
}
