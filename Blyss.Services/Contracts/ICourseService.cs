namespace Blyss.Services.Contracts
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface ICourseService
    {

        Task<IEnumerable<Course>> GetAllCourses();

        Task<IEnumerable<Course>> GetAllCoursesWithAuthors();

        Task<bool> CreateCourse(User user, string name, string description, string category, string language,
            string youTubeId);

        Task<Course> GetCourseById(string id);

        Task DeleteCourse(Course course);

        Task<Course> GetCourseWithUserAndVideoById(string id);

        Task EditCourse(Course course, string name, string description, string category, string language, string video);

        Task ApproveCourse(Course course);

        Task<Course> GetCourseWithCategoryLanguageAndUserById(string id);

    }

}