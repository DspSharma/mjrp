using System.ComponentModel.DataAnnotations;

namespace MJRPAdmin.DTO.DtoInput
{
    public class UserInput
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required field.")]
        [RegularExpression(@"^([a-zA-Z]+)(\s[a-zA-Z]+)*$", ErrorMessage = "Not a valid Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required field.")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Not a valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile Number is required field.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Password is required field.")]
        public string Password { get; set; }
        public int UserType { get; set; } = 1;
       
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
