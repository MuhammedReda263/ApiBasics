using System.ComponentModel.DataAnnotations;

namespace WebApiProject.DTO
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }

        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
    }
}
