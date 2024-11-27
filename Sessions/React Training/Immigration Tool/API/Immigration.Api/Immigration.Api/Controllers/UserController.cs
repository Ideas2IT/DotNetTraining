using Immigration.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Immigration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        static UserData _userData;
        public UserController  ()
        {
            _userData = new UserData();
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {  
            return _userData.Users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userData.Users.Where(u=>u.Id == id).FirstOrDefault();
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] User value)
        {
            _userData.Users.Add(value);
        }       
    }
}
