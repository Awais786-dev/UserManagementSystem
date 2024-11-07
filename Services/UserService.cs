using User_Management_System.Models;
using User_Management_System.Repositories;
using System;
using System.Collections.Generic;
using User_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using User_Management_System.DTOs;
using AutoMapper;

namespace User_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepos;
        private readonly IMapper _mapper;
        public UserService(UserRepository userRepos, IMapper mapper)
        {
            _userRepos = userRepos;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepos.GetAllUsers();  // Get users from the repository
            var userDtos = _mapper.Map<List<UserDto>>(users); // Map users to UserDto using AutoMapper
            return userDtos;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _userRepos.GetUserById(id); // Get user by ID from repository
            if (user == null)
            {
                return null; // Return null if user not found
            }
            return _mapper.Map<UserDto>(user); // Map User entity to UserDto
        }
        public async Task<UserDto> AddUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _userRepos.AddNewUser(user);

            var savedUserDto = _mapper.Map<UserDto>(user);
            return savedUserDto;
        }

        public async Task<UserDto> UpdateUser(UserDto userDto)
        {
            try
            {
                // Ensure the user ID from the DTO is valid
                if (!userDto.Id.HasValue || userDto.Id.Value <= 0)
                {
                    throw new Exception("Invalid user ID.");
                }

                // Map UserDto to User entity
                var user = _mapper.Map<User>(userDto);

                // Call the repository to update the user
                _userRepos.UpdateUser(userDto.Id.Value, user);

                // After the update, map the updated User back to UserDto
                var updatedUserDto = _mapper.Map<UserDto>(user);

                return updatedUserDto;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user.", ex);
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Invalid user ID.");
                }
                _userRepos.DeleteUser(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }
    }

}
