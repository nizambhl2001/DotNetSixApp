using DotNetSixApp.Data;
using DotNetSixApp.Dto;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace DotNetSixApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public DataContext _dapper;
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
           _config = config;
            _dapper = new DataContext(config);
        }
        [HttpPost("Registraion")]
        public IActionResult Registaion(RegistraionDto registraionDto)
        {
            if (registraionDto.PassWord == registraionDto.PassWordConfirm)
            {
                string sqlChickUserExist = "select Email from Users where  Email = '" + registraionDto.Email + "'";
                IEnumerable<string> existUser = _dapper.LoadDatas<string>(sqlChickUserExist);
                if (existUser.Count() == 0)
                {
                    byte[] PasswordSalt = new byte[128 / 8];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(PasswordSalt);
                    }
                    string PasswordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value
                        + Convert.ToBase64String(PasswordSalt);

                    byte[] PasswordHash = KeyDerivation.Pbkdf2(
                        password: registraionDto.PassWord,
                        salt: Encoding.ASCII.GetBytes(PasswordSaltPlusString),
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 1000000,
                        numBytesRequested: 256 / 8

                        );
                    string sqlAuth = @"insert into dbo.Auth ( Email, PasswordHash, PasswordSalt)values ('" + registraionDto.Email + "',@PasswordHash,@PasswordSalt)";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    SqlParameter PasswordSaltParameters = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
                    PasswordSaltParameters.Value = PasswordSalt;

                    SqlParameter PasswordHashParameters = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
                    PasswordHashParameters.Value = PasswordHash;

                    sqlParameters.Add(PasswordSaltParameters);
                    sqlParameters.Add(PasswordHashParameters);
                    if (_dapper.ExecuteSqlWithParameter(sqlAuth, sqlParameters))
                    {
                        return Ok();
                    }
                    return BadRequest("Failed to Register User.");

                }
                return BadRequest("User Alreay exist !");
            }
            return BadRequest("PassWord Do Not Match");
        }

        [HttpPost("LogIn")]
        public IActionResult Login(UserLogInDto userLogInDto)
        {

            return Ok();
        }

       
    }
}
