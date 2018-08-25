namespace Blyss.Web.Models.BindingModels
{

    using System.ComponentModel.DataAnnotations;

    public class RegisterBindingModel
    {

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(3, ErrorMessage = "{0} must be more than {1} symbols")]
        [MaxLength(12, ErrorMessage = "{0} can't be more than {1} symbols")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "E-mail")]
        [RegularExpression(
            @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "Invalid {0} address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(6, ErrorMessage = "{0} must be more than {1} symbols")]
        [MaxLength(32, ErrorMessage = "{0} can't be more than {1} symbols")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; }

    }

}