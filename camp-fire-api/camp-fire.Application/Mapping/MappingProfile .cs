using AutoMapper;
using camp_fire.Application.Models;
using camp_fire.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Experience, ExperienceResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Box, opt => opt.MapFrom(src => new BoxResponse { Id = src.BoxId.Value }))
            .ForMember(dest => dest.Agendas, opt => opt.MapFrom(src => src.AgendaIds.Select(id => new AgendaResponse { Id = id })));

        CreateMap<CreateExperienceRequest, Experience>();

        CreateMap<UpdateExperienceRequest, Experience>();
    }
}