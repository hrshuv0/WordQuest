using AutoMapper;
using Core.Dtos;
using Core.Dtos.Identity;
using Core.Entities.Identity;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ApplicationUser, UserDetailsDto>();
        
        CreateMap<ApplicationUser, UserDetailsForReturnDto>();
        
        CreateMap<ApplicationUser, UserListDto>();

    }
}
