using System.ComponentModel.DataAnnotations;

namespace TodoListApp.WebApi.Model
{
    public class LoginModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum length must be 4 characters.")]
        public string Password { get; set; }
    }
}
