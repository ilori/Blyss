namespace Blyss.Web.Models.BindingModels
{

    using System.ComponentModel.DataAnnotations;

    public class ForgottenPasswordBindingModel
    {

        [Required(ErrorMessage = "Please enter E-Mail or Username")]
        public string Identifier { get; set; }

    }

}