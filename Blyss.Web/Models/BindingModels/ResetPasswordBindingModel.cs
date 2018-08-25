namespace Blyss.Web.Models.BindingModels
{

    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordBindingModel
    {

        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "New password")]
        [MinLength(6, ErrorMessage = "{0} must be more than {1} symbols")]
        [MaxLength(32, ErrorMessage = "{0} can't be more than {1} symbols")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; }

    }

}