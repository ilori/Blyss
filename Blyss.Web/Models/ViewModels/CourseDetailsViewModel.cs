namespace Blyss.Web.Models.ViewModels
{

    using System;

    public class CourseDetailsViewModel
    {

        public string CourseId { get; set; }

        public string CourseCategory { get; set; }

        public string CourseLanguage { get; set; }

        public string Video { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CourseDescription { get; set; }

        public string User { get; set; }

        public string UserId { get; set; }

        public string UserDescription { get; set; }

        public byte[] ProfilePicture { get; set; }

        public string Country { get; set; }

    }

}