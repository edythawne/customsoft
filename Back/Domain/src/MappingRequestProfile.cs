using AutoMapper;
using Domain.Request.App;
using Domain.Request.Auth;
using Infrastructure.Dto.App;
using Infrastructure.Dto.Auth;

namespace Domain;

public class MappingRequestProfile : Profile {
    
    public MappingRequestProfile() {
        CreateMap<LoginRequest, LoginDto>();
        CreateMap<UserPaginatedRequest, UserPaginatedDto>();
        
        CreateMap<CreateUserRequest, CreateUserDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
        
        CreateMap<CreateUserDetailRequest, CreateUserDetailDto>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
    }
    
}