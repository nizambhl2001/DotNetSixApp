namespace DotNetSixApp.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }
       
    }

    public class UsersJobInfo
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public string JobTitle { get; set; }
        public string Depertment { get; set; }
 

    } 
    
    public class UsersSalary
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public decimal Salary { get; set; }
        public decimal AvgSalary { get; set; }
 

    }
}
