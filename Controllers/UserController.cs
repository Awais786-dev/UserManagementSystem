using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using User_Management_System.DTOs;
using User_Management_System.Entities;
using User_Management_System.Models;
using User_Management_System.Repositories;
using User_Management_System.Responses;
using User_Management_System.Services;

namespace User_Management_System.Controllers
{
    [Route("api/[controller]")]
   // [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;     
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
       // [Route("get")]
        public async Task<ActionResult<ApiResponse<List<UserDto>>>> GetAllUsers()
        {
            var userDtos = await _userService.GetAllUsers(); // Get the DTOs from the service

            if (userDtos == null || !userDtos.Any()) // Check if no users were found
            {
                // Return a NotFound response with a message
                return NotFound(new ApiResponse<List<UserDto>>("No users found."));
            }

            // Return a success response with the user list and success message
            return Ok(new ApiResponse<List<UserDto>>(userDtos, "Users retrieved successfully."));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Find(int id)
        {
            try
            {
                var userDto = await _userService.GetUserById(id); // Get user by ID from the service
                if (userDto == null)
                {
                    return NotFound(new ApiResponse<UserDto>("User not found with the specified ID."));
                }

                return Ok(new ApiResponse<UserDto>(userDto, "User retrieved successfully."));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ApiResponse<UserDto>(ex.Message)); // Handle exceptions
            }
        }

        [HttpPost]
      //  [Route("add")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(new ApiResponse<UserDto>("Invalid user data", new List<string> { "Request body is empty or malformed." }));
            }

            // Check if the model is valid (Automatic validation through annotations)
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();

                // Collect all validation errors from ModelState (these come from data annotations like [Required])
                foreach (var error in ModelState.Values)
                {
                    foreach (var subError in error.Errors)
                    {
                        errors.Add(subError.ErrorMessage);
                    }
                }

                return BadRequest(new ApiResponse<UserDto>("Invalid user data", errors));
            }

            try
            {
                var savedUserDto = await _userService.AddUser(userDto);
                return Ok(new ApiResponse<UserDto>(savedUserDto, "User created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<UserDto>("An error occurred while creating the user", new List<string> { ex.Message }));
            }
        }




        //updated update controller
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Update(int id, [FromBody] UserDto userDto)
        {
            try
            {
                // Ensure the ID in the URL matches the ID in the request body
                if (userDto.Id.HasValue && userDto.Id.Value != id)
                {
                    return BadRequest(new ApiResponse<UserDto>("The provided ID in the URL does not match the ID in the request body."));
                }

                // Set the ID in the DTO to ensure it's mapped correctly
                userDto.Id = id;

                // Call the service to update the user
                var updatedUserDto = await _userService.UpdateUser(userDto);

                // Return the updated UserDto in the response
                return Ok(new ApiResponse<UserDto>(updatedUserDto, "User updated successfully."));
            }
            catch (Exception ex)
            {
                // Return error if an exception occurs
                return BadRequest(new ApiResponse<UserDto>("An error occurred while updating the user.", new List<string> { ex.Message }));
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<string>> Delete(int id)
        {
            try
            {
                // Call the service to delete the user
                _userService.DeleteUser(id);

                // Return success response
                return Ok(new ApiResponse<string>(null, "User deleted successfully.")); // Set success = true implicitly
            }
            catch (Exception ex)
            {
                // Return error if an exception occurs
                return BadRequest(new ApiResponse<string>("An error occurred while deleting the user.", new List<string> { ex.Message }));
            }
        }


    }
}
