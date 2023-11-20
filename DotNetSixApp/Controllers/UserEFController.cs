using DotNetSixApp.Data;
using DotNetSixApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetSixApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEFController : ControllerBase
    {
        EFContext _config;
        public UserEFController(IConfiguration config)
        {
            _config = new EFContext(config);
        }
    
      
        [HttpGet]
        public async Task<IEnumerable<Users>> GetUser()
        {

            IEnumerable<Users> user = await _config.Users.ToListAsync<Users>();
            return user;
        }

        [HttpGet("GetUser/{userId}")]
        public async Task<ActionResult<Users>> GetUser(int userId)
        {
           
            Users user = await _config.Users.Where(x=>x.UserId == userId).FirstOrDefaultAsync<Users>();
            if(user != null) { return user; }
            throw new Exception("User Not Found");
        }

        [HttpPut("EitUser")]
        public async Task<IActionResult> EitUser(Users users)
        {
            Users userDto = await _config.Users.Where(u=>u.UserId == users.UserId).FirstOrDefaultAsync();   
           
            if(userDto != null)
            {
                userDto.FirstName = users.FirstName;
                userDto.LastName = users.LastName;
                userDto.Gender = users.Gender;
                userDto.Email = users.Email;
                userDto.Active = users.Active;

                if(await _config.SaveChangesAsync()>0) { return Ok(); }
                
            }

            throw new Exception("Fail Update new user");
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(Users users)
        {
            await _config.Users.AddAsync(users);
            await _config.SaveChangesAsync();
           return Ok();
            
          

            throw new Exception("Fail Update new user");
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            Users? userDto = await _config.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();
            if (userDto != null)
            {
                _config.Users.Remove(userDto);
                if(await _config.SaveChangesAsync() > 0)
                {
                    return Ok();
                }
                throw new Exception("Fail to Delete user");

            }
            throw new Exception("Fail to Get user");
        }


    }
}
