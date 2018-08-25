namespace Blyss.Services
{

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {

        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await this.userManager.Users.ToListAsync();
        }

        public async Task<User> GetUserByUsername(string userName)
        {
            return await this.userManager.FindByNameAsync(userName);
        }

        public async Task<User> GetUserWithCoursesById(string id)
        {
            return await this.userManager.Users.Include(x => x.Courses).ThenInclude(x => x.Language)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserWithCoursesAndVideosById(string id)
        {
            return await this.userManager.Users.Include(x => x.Courses).ThenInclude(x => x.Video)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<User> GetAllUsersWithCourses()
        {
            return this.userManager.Users.Include(x => x.Courses).ToList();
        }

    }

}