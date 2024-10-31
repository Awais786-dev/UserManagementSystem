namespace UserManagementSystem.Models
{
    public class UserRepos
    {
        private List<User> users;
        public UserRepos()
        {
            users = new List<User>()  {
                new User() { Id=1, Name="M Awais", Age=21 },
                new User() { Id=2, Name="Ali Khan", Age=27 },
                new User() { Id=3, Name="Tahir Mustasfvi", Age=20 },
                new User() { Id=4, Name="Abdullah", Age=23 }

            };
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public User GetUserById(int id)
        {
            return users.Find(s => s.Id == id);
        }

        public void AddNewUser(User user)
        {
            // Check if the user object is null
            if (user == null)
            {
                throw new Exception("User cannot be null.");
            }

            // Check if the ID is less than or equal to zero
            if (user.Id <= 0)
            {
                throw new Exception("User ID must be a positive number.");
            }

            // Check if the Name is null or empty
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new Exception("User Name cannot be empty.");
            }

            // Check if the Age is less than or equal to zero
            if (user.Age <= 0)
            {
                throw new Exception("User Age must be a positive number.");
            }

            // Check if a user with the same ID already exists
            if (GetUserById(user.Id) != null)
            {
                throw new Exception("User with this ID already exists.");
            }

            users.Add(user);
        }


        public void UpdateUser(int id, User updatedUser)
        {
            if (id <= 0)
            {
                throw new Exception("User ID must be provided and must be a positive integer.");
            }
            // Check if the user exists
            User existingUser = GetUserById(id);

            if (existingUser != null)
            {
                // Check if the ID in the request matches the ID in the updated user
                if (updatedUser.Id != 0 && existingUser.Id != updatedUser.Id)
                {
                    throw new Exception("User ID in the request body does not match the ID in the URL.");
                }

                // Check if name is empty
                if (string.IsNullOrWhiteSpace(updatedUser.Name))
                {
                    throw new Exception("Name cannot be empty.");
                }

                // Check if age is valid
                if (updatedUser.Age < 18)
                {
                    throw new Exception("Age must be 18 or older.");
                }
                // Update user details
                existingUser.Name = updatedUser.Name;
                existingUser.Age = updatedUser.Age;
            }
            else
            {
                throw new Exception("User not found.");
            }
        }


        public void DeleteUser(int id)
        {
            User userToRemove = GetUserById(id);
            if (userToRemove != null)
            {
                users.Remove(userToRemove);
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}
