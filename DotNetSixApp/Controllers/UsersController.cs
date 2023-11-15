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

     
    }
}
