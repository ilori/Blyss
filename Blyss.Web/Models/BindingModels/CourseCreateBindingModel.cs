namespace Blyss.Web.Models.BindingModels
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CourseCreateBindingModel
    {

        public List<string> Languages { get; set; }

        public List<string> Categories { get; set; }

        [MinLength(5, ErrorMessage = "{0} must be at least {1} symbols long")]
        [MaxLength(13, ErrorMessage = "{0} must be less than {1} symbols")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(12, ErrorMessage = "{0} must be at least {1} symbols long")]
        [MaxLength(45, ErrorMessage = "{0} must be less than {1} symbols")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Language { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Invalid {0}.")]
        [Display(Name = "YouTube Id")]
        public string Video { get; set; }

    }

}