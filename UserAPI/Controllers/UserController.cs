using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        static List<User> users = new List<User> {
            new User("John", "Doe", 24),
            new User("Jane", "Doe", 23),
            new User("Bob", "Bobsky", 26),
            new User("Jimmy", "Neutron", 14)
        };

        [HttpGet()]
        public List<User> Index()
        {
            return users;
        }

        [HttpGet("{i}", Name = "GetByIndex")]
        //[EnableCors]
        public ActionResult<User> GetByIndex([FromRoute] int i)
        {
            if (i < 0) return BadRequest();
            
            User u = users.ElementAtOrDefault(i);
            if (u == null) return NotFound();

            return Ok(u);
        }

        [HttpGet("isAdult/{i}")]
        public ActionResult<bool> IsAdult([FromRoute] int i)
        {
            if (i < 0) return BadRequest();
            
            User u = users.ElementAtOrDefault(i);
            if (u == null) return NotFound();

            return Ok(u.Age >= 18);
        }

        [HttpPost("create")]
        [EnableCors]
        public ActionResult<User> CreateUser([FromForm] User u)
        {
            users.Add(u);
            //return Ok(u);
            return CreatedAtRoute("GetByIndex", new { i = users.Count - 1 }, u);
        }

        // ?colors=a&colors=b... => {a,b,...}
        [HttpGet("getlen")]
        public int GetQueryArrayLen([FromQuery] string[] colors)
        {
            return colors.Length;
        }
    }
}
