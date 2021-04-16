using API.DTOs;
using API.Entities;
using API.Helpers.Pagination;
using AutoMapper;

namespace API.Helpers
{
    public partial class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            AddGeneratedMaps();

            CreateMap<RegisterDto, AppUser>();

            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, UserAdminDto>();
            CreateMap<AppUser, MemberDto>();

            CreateMap<PaginationParams, PaginationHeader>();
        }
    }
}