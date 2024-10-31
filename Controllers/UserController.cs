using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Models;


namespace UserManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private static UserRepos _userRepos = new UserRepos(); 

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