using System;
using API.DTOs;
using API.Entities;
using API.Helpers.Pagination;
using API.Helpers.Pagination.Custom;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, AppUser>();

            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, UserAdminDto>();
            CreateMap<AppUser, MemberDto>();

            CreateMap<PaginationParams, PaginationHeader>();
            
            CreateMap<TestParams, TestHeader>();
        }
    }
}