using DotNetSixApp.Controllers.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DotNetSixApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        DataContext _config;

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
        public string[] UserGet(string User)
        {
            return new string[]
            {
                "User1",
                "User2",
                User
            };

        }
    }
}
