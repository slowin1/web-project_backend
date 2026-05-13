using AutoMapper;
using eUseControl.Domain.DTOs;
using eUseControl.Domain.Entities.User;

namespace eUseControl.BussinessLogic;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserData, UserResponseDto>();
        CreateMap<CreateUserDto, UserData>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.RegisteredOn, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}