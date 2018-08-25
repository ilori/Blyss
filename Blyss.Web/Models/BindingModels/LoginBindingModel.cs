namespace Blyss.Web.Models.BindingModels
{

    using System.ComponentModel.DataAnnotations;

    public class LoginBindingModel
    {

        [Required(ErrorMessage = "{0} is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }

}