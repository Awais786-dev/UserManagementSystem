using Microsoft.EntityFrameworkCore;
using User_Management_System.Entities;
using User_Management_System.Models;

namespace User_Management_System.Repositories
{
    public class UserRepository
    {
        // List<User> users;
        private readonly EFCoreDbContext _dbcontext;
        private List<User> users;
                           
        public UserRepository(EFCoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task<List<User>> GetAllUsers()
        {
            return await _dbcontext.Users.ToListAsync(); // Fetch all users from the Users table
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbcontext.Users.FindAsync(id); // Finds a user by ID
        }

        // Add a new user to the database
        public async Task AddNewUser(User user)
        {
            // Check if the user object is null
            if (user == null)
            {
                throw new Exception("User cannot be null.");
            }

            // Validate the Name field
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new Exception("User Name cannot be empty.");
            }

            // Validate the Age field
            if (user.Age <= 0)
            {
                throw new Exception("User Age must be a positive number.");
            }

            // Validate the Address field
            if (string.IsNullOrWhiteSpace(user.Address))
            {
                throw new Exception("User Address cannot be empty.");
            }

            //  Check if a user with the same name already exists (if that's required)
            if (await _dbcontext.Users.AnyAsync(u => u.Name == user.Name))
            {
                throw new Exception("A user with the same name already exists.");
            }

            // Add user to the database (auto-generated Id will be handled by the database)
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync(); // Save changes to the database
        }

        public void UpdateUser(int id, User updatedUser)
        {
            if (id <= 0)
            {
                throw new Exception("User ID must be provided and must be a positive integer.");
            }

            // Get the existing user from the database
            User existingUser = _dbcontext.Users.FirstOrDefault(u => u.Id == id);

            if (existingUser != null)
            {
                // Check if the ID in the request body matches the ID in the URL
                if (updatedUser.Id != 0 && existingUser.Id != updatedUser.Id)
                {
                    throw new Exception("User ID in the request body does not match the ID in the URL.");
                }

                // Update only the provided fields (leave the rest unchanged)

                if (!string.IsNullOrWhiteSpace(updatedUser.Name))
                    existingUser.Name = updatedUser.Name;

                if (updatedUser.Age > 0)
                    existingUser.Age = updatedUser.Age;

                if (!string.IsNullOrWhiteSpace(updatedUser.Address))
                    existingUser.Address = updatedUser.Address;

                // Save changes to the database
                _dbcontext.SaveChanges();
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        public void DeleteUser(int id)
        {
            if (id <= 0)
            {
                throw new Exception("User ID must be a valid and positive integer.");
            }

            // Find the user by Id
            var user = _dbcontext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Remove the user from the database
            _dbcontext.Users.Remove(user);
            _dbcontext.SaveChanges();
        }
    }
}
