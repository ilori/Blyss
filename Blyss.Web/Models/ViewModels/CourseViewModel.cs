namespace Blyss.Web.Models.ViewModels
{

    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Helpers;

    public class CourseViewModel
    {

        public List<string> Languages { get; set; }

        public List<string> Categories { get; set; }

        public IQueryable<Course> Courses { get; set; }

        public List<string> Authors { get; set; }

        public PaginatedList<Course> Paged { get; set; }

    }

}