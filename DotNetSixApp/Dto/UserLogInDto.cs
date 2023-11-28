namespace DotNetSixApp.Dto
{
    public class UserLogInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
       

    }

    public class UserForLoginConframDto
    {
         public  byte[] PasswordHash { get; set; }
         public  byte[] PasswordSalt { get; set; }
    }
}
