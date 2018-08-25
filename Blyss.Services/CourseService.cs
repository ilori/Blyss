namespace Blyss.Services
{

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class CourseService : ICourseService
    {

        private readonly BlyssContext context;

        public CourseService(BlyssContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await this.context.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesWithAuthors()
        {
            return await this.context.Courses.Include(x => x.User).ToListAsync();
        }

        public async Task<bool> CreateCourse(User user, string name, string description, string category,
            string language, string youTubeId)
        {
            if (await this.context.Courses.AnyAsync(x => x.Name == name) ||
                await this.context.Videos.AnyAsync(x => x.PlainYoutubeId == youTubeId))
            {
                return false;
            }

            if (user == null)
            {
                return false;
            }

            Course course = new Course
            {
                Name = name,
                Description = description,
                Category = await this.context.Categories.SingleOrDefaultAsync(x => x.Name == category),
                Language = await this.context.Languages.SingleOrDefaultAsync(x => x.Name == language),
                CreatedOn = DateTime.Today,
                Video = new Video
                {
                    YouTubeId = $"https://www.youtube.com/embed/{youTubeId}?rel=0&amp;showinfo=0",
                    PlainYoutubeId = youTubeId
                }
            };

            user.Courses.Add(course);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Course> GetCourseById(string id)
        {
            return await this.context.Courses.Include(x => x.User).Include(x => x.Video)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Course> GetCourseWithCategoryLanguageAndUserById(string id)
        {
            return await this.context.Courses.Include(x => x.User).Include(x => x.Language).Include(x => x.Category)
                .Include(x => x.Video)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteCourse(Course course)
        {
            this.context.Courses.Remove(course);

            await this.context.SaveChangesAsync();
        }

        public async Task<Course> GetCourseWithUserAndVideoById(string id)
        {
            return await this.context.Courses.Include(x => x.Video).Include(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task EditCourse(Course course, string name, string description, string category, string language,
            string video)
        {
            course.Name = name;
            course.Description = description;
            course.Category = await this.context.Categories.SingleOrDefaultAsync(x => x.Name == category);
            course.Language = await this.context.Languages.SingleOrDefaultAsync(x => x.Name == language);
            course.Video.PlainYoutubeId = video;
            course.Video.YouTubeId = $"https://www.youtube.com/embed/{video}?rel=0&amp;showinfo=0";
            course.IsApproved = false;
            await this.context.SaveChangesAsync();
        }

        public async Task ApproveCourse(Course course)
        {
            course.IsApproved = true;

            await this.context.SaveChangesAsync();
        }

    }

}