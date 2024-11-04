using Microsoft.AspNetCore.Mvc;
using Updated_UserManagementSystem.Models;
using Updated_UserManagementSystem.Repositories;

namespace Updated_UserManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private static UserRepository _userRepos = new UserRepository();

        [HttpGet]
        [Route("get")]
        public List<User> Get()
        {
            return _userRepos.GetAllUsers();
        }


        [HttpGet]
        // [HttpGet("{id}")] //we can also use it
        [Route("find/{id}")]
        public User Get(int id)
        {
            return _userRepos.GetUserById(id);
        }



        [HttpPost]
        [Route("Add")]
        public List<User> Add(User user)
        {
            _userRepos.AddNewUser(user);
            return _userRepos.GetAllUsers();
        }


        [HttpPut("{id}")]
        // [Route("update")]
        public List<User> Update(User user, int id)
        {
            _userRepos.UpdateUser(id, user);
            return _userRepos.GetAllUsers();
        }


        [HttpDelete("{id}")]
        // [Route("update")]
        public List<User> deleteUser(int id)
        {
            _userRepos.DeleteUser(id);
            return _userRepos.GetAllUsers();
        }

    }
}
