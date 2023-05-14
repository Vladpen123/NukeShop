using System.ComponentModel.DataAnnotations;

namespace NukeShop.WebUI.ViewModels
{

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter your e-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Enter your surname")]
        public string? LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string? PasswordConfirm { get; set; }
    }
}