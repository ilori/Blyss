namespace Blyss.Web.Models.BindingModels
{

    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class UserSettingsBindingModel
    {

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "First Name")]
        [MaxLength(15, ErrorMessage = "{0} must be less than {1} symbols")]
        [MinLength(2, ErrorMessage = "{0} must be at least {1} symbols long.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Last Name")]
        [MaxLength(15, ErrorMessage = "{0} must be less than {1} symbols")]
        [MinLength(3, ErrorMessage = "{0} must be at least {1} symbols long.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(45, ErrorMessage = "{0} must be less than {1} symbols")]
        [MinLength(6, ErrorMessage = "{0} must be at least {1} symbols long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Picture")]
        public IFormFile ProfilePicture { get; set; }

    }

}