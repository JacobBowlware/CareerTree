using AutoMapper;
using CareerAPI.Models;
using CareerAPI.DTOs;

namespace CareerAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.ToList()));
        }
    }
}
