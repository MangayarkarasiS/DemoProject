using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuth jwtAuth;
        private readonly List<User> lstUser = new List<User>()
        {
            new User{Id=1, Name="Siddhu" ,Role="admin"},
            new User {Id=2, Name="Nitya",Role="admin" },
            new User{Id=3, Name="Bhargav",Role="user"}
        };
        public UserController(IAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }
        // GET: api/<UserController>
        [Authorize(Roles ="admin")]
        [HttpGet]
        public IEnumerable<User> GetAllusers()
        {
            return lstUser;
        }

        // GET api/<UserController>/5
        [Authorize(Roles = "user")]
        [HttpGet("{id}")]
        public User UserById(int id)
        {
            return lstUser.Find(x => x.Id == id);
        }

        [AllowAnonymous]
        // POST api/<UserController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }


    }
}

//angular - front end    web api - backend


//api/uer/authentication
//username,pwd - browser
//server - validating, token, send it to browser


//browser - receiving, if it wants to get user data - it will send the token
// server - validate token and give the user data









