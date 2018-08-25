namespace Blyss.Web.Models.ViewModels
{

    using System.Collections.Generic;
    using Entities;
    using Helpers;

    public class UserDetailsViewModel
    {

        public string FullName { get; set; }

        public byte[] ProfilePicture { get; set; }

        public List<Course> Courses { get; set; }

        public PaginatedList<Course> Paged { get; set; }

    }

}