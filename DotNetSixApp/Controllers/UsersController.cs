using DotNetSixApp.Data;
using DotNetSixApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DotNetSixApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public DataContext _config;

        public UsersController(IConfiguration config)
        {
            _config = new DataContext(config);
        }
        [HttpGet("Test")]
        public async Task<DateTime> Test()
        {
            return await _config.LoadDataSingle<DateTime>("Select GETDATE()");
        }

        [HttpGet]
        public async Task<IEnumerable<Users>> GetUser()
        {
            string sql = "SELECT * FROM Users";

            IEnumerable<Users> user =await _config.LoadData<Users>(sql);
            return user;
        }

        [HttpGet("GetUser/{userId}")]
        public async Task<Users> GetUser(int userId)
        {
            string sql = "SELECT * FROM Users where UserId = " +userId;

            Users user =await _config.LoadDataSingle<Users>(sql);
            return user;
        } 
        [HttpPut("EitUser")]
        public  IActionResult EitUser(Users users)
        {
            string sql = @"Update Users
                        Set FirstName= '" + users.FirstName +
                        "', LastName= '" + users.LastName +
                        "', Email= '" + users.Email +
                        "', Gender= '" + users.Gender +
                        "', Active = '" + users.Active +
                        "' where UserId = " + users.UserId;

            if(_config.ExecuteSql(sql)) { return Ok(); }

            throw new Exception("Fail Update new user");
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(Users users)
        {
            string sql = @"Insert into Users(
                        FirstName,
                        LastName,
                        Email, 
                        Gender,
                        Active
                        ) values (
                        '" + users.FirstName +
                        "', '" + users.LastName +
                        "', '" + users.Email +
                        "', '" + users.Gender +
                        "', '" + users.Active +
                        "')";

            if (_config.ExecuteSql(sql)) { return Ok(); }

            throw new Exception("Fail Update new user");
        }


    }
}
