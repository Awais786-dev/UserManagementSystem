using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Updated_UserManagementSystem.DTOs;
using Updated_UserManagementSystem.Models;

namespace Updated_UserManagementSystem.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // CreateMap<User, UserDto>(); // Map User to UserDto
            CreateMap<User, UserDto>()
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name));

            //CreateMap<UserDto, User>(); // Map UserDto back to User
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));


        }
    }
}
