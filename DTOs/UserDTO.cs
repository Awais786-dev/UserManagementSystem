namespace Updated_UserManagementSystem.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } // Assuming you want to map Name to FullName
        public int Age { get; set; }

        public string Address { get; set; }
    }
}
