using User_Management_System.Models;
using System.Collections.Generic;
using User_Management_System.DTOs;

namespace User_Management_System.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task<UserDto> AddUser(UserDto userDto);
        Task<UserDto> UpdateUser(UserDto userDto);
         void DeleteUser(int id);
    }
}