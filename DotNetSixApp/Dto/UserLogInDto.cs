namespace DotNetSixApp.Dto
{
    public class UserLogInDto
    {
      public  byte[] PasswordHash { get; set; }
      public  byte[] PasswordSalt { get; set; }

    }
}
