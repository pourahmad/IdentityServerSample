using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientAspNetCoreWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {

        }


        [HttpGet(nameof(GetUsers))]
        public IActionResult GetUsers()
        {
            List<userDto> users = new List<userDto>
            {
                new userDto{Id = 1, Name = "user1"},
                new userDto{Id = 2, Name = "user2"},
                new userDto{Id = 3, Name = "user3"},
                new userDto{Id = 4, Name = "user4"},
                new userDto{Id = 5, Name = "user5"},
                new userDto{Id = 6, Name = "user6"},
            };

            return Ok(users);
        }


        [HttpGet("id")]
        public IActionResult GetUserById(int id)
        {
            return Ok();
        }
    }

    public class userDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
