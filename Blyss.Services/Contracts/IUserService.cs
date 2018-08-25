namespace Blyss.Services.Contracts
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IUserService
    {

        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserByUsername(string userName);

        Task<User> GetUserWithCoursesById(string id);

        Task<User> GetUserWithCoursesAndVideosById(string id);

        IEnumerable<User> GetAllUsersWithCourses();

    }

}