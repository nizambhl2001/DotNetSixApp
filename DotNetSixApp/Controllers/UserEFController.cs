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

        //[HttpGet("GetUser/{userId}")]
        //public async Task<ActionResult<Users>> GetUser(int userId)
        //{

        //    string sql = "SELECT * FROM Users where UserId = " + userId;
        //    Users user = await _config.LoadDataSingle<Users>(sql);
        //    return user;
        //}

        //[HttpPut("EitUser")]
        //public async Task<IActionResult> EitUser(Users users)
        //{
        //    string sql = @"Update Users
        //                Set FirstName= '" + users.FirstName +
        //                "', LastName= '" + users.LastName +
        //                "', Email= '" + users.Email +
        //                "', Gender= '" + users.Gender +
        //                "', Active = '" + users.Active +
        //                "' where UserId = " + users.UserId;

        //    if (await _config.ExecuteSql(sql)) { return Ok(); }

        //    throw new Exception("Fail Update new user");
        //}

        //[HttpPost("AddUser")]
        //public async Task<IActionResult> AddUser(Users users)
        //{
        //    string sql = @"Insert into Users(
        //                FirstName,
        //                LastName,
        //                Email, 
        //                Gender,
        //                Active
        //                ) values (
        //                '" + users.FirstName +
        //                "', '" + users.LastName +
        //                "', '" + users.Email +
        //                "', '" + users.Gender +
        //                "', '" + users.Active +
        //                "')";

        //    if (await _config.ExecuteSql(sql)) { return Ok(); }

        //    throw new Exception("Fail Update new user");
        //}

        //[HttpDelete("DeleteUser/{userId}")]
        //public async Task<IActionResult> DeleteUser(int userId)
        //{
        //    string sql = @"Delete from Users where UserId =" + userId;

        //    if (await _config.ExecuteSql(sql)) { return Ok(); }

        //    throw new Exception("Fail Update new user");
        //}


    }
}
