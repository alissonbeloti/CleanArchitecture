using AutoMapper;

using CleanArchitecture.Domain;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Features.Director.Commands.CreateDirector;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

namespace CleanArchitecture.Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Video, VideosVm>().ReverseMap();
        CreateMap<CreateStreamerCommand, Streamer>().ReverseMap();
        CreateMap<UpdateStreamerCommand, Streamer>().ReverseMap();
        CreateMap<CreateDirectorCommand, Director>().ReverseMap();
    }
}
