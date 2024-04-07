using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
      
    }
}
