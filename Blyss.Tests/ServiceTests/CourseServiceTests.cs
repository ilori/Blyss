namespace Blyss.Tests.ServiceTests
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Entities;
    using Mocks;
    using NUnit.Framework;
    using Services;
    using Services.Contracts;

    [TestFixture]
    public class CourseServiceTests
    {

        [SetUp]
        public void InitializeDbContext()
        {
            this.context = MockDbContext.GetContext();
            this.service = new CourseService(this.context);
        }

        private BlyssContext context;

        private ICourseService service;

        [Test]
        public async Task ShouldCreateCourse()
        {
            this.context.Users.Add(new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "magix",
                Email = "magix@abv.bg",
                PasswordHash = "asdasd",
                Country = "Bulgaria"
            });

            await this.context.SaveChangesAsync();

            User user = this.context.Users.First();

            bool result =
                await this.service.CreateCourse(user, "ASP NET CORE", "ASP NET CORE", "C#", "Bulgarian", "12345678901");

            Assert.That(result, Is.True);
            Assert.That(this.context.Courses.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task ShouldDeleteCourse()
        {
            this.context.Users.Add(new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "magix",
                Email = "magix@abv.bg",
                PasswordHash = "asdasd",
                Country = "Bulgaria"
            });

            await this.context.SaveChangesAsync();

            User user = this.context.Users.First();

            bool result =
                await this.service.CreateCourse(user, "ASP NET CORE", "ASP NET CORE", "C#", "Bulgarian", "12345678901");

            Assert.That(result, Is.True);
            Assert.That(this.context.Courses.Count(), Is.EqualTo(1));

            Course course = this.context.Courses.First();

            await this.service.DeleteCourse(course);

            Assert.That(this.context.Courses.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task ShouldNotCreateCourse()
        {
            bool result =
                await this.service.CreateCourse(null, "ASP NET CORE", "ASP NET CORE", "C#", "Bulgarian", "12345678901");

            Assert.That(result, Is.False);
            Assert.That(this.context.Courses.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task ShouldNotCreateTwoCoursesWithTheSameName()
        {
            this.context.Users.Add(new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "magix",
                Email = "magix@abv.bg",
                PasswordHash = "asdasd",
                Country = "Bulgaria"
            });

            await this.context.SaveChangesAsync();

            User user = this.context.Users.First();

            bool result =
                await this.service.CreateCourse(user, "ASP NET CORE", "ASP NET CORE", "C#", "Bulgarian", "12345678901");

            Assert.That(result, Is.True);
            Assert.That(this.context.Courses.Count(), Is.EqualTo(1));

            bool secondResult =
                await this.service.CreateCourse(user, "ASP NET CORE", "FAKE DATA FOR COURSE", "C#", "Bulgarian",
                    "12345678901");

            Assert.That(secondResult, Is.False);
            Assert.That(this.context.Courses.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task ShouldNotCreateTwoCoursesWithTheSameVideoId()
        {
            this.context.Users.Add(new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "magix",
                Email = "magix@abv.bg",
                PasswordHash = "asdasd",
                Country = "Bulgaria"
            });

            await this.context.SaveChangesAsync();

            User user = this.context.Users.First();

            bool result =
                await this.service.CreateCourse(user, "ASP NET CORE", "ASP NET CORE", "C#", "Bulgarian", "12345678901");

            Assert.That(result, Is.True);
            Assert.That(this.context.Courses.Count(), Is.EqualTo(1));

            bool secondResult =
                await this.service.CreateCourse(user, "DIFFERENT NAME", "DIFFERENT DESCRIPTION", "C#", "Bulgarian",
                    "12345678901");

            Assert.That(secondResult, Is.False);
            Assert.That(this.context.Courses.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task ShouldReturnAllCourses()
        {
            this.context.Users.Add(new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "magix",
                Email = "magix@abv.bg",
                PasswordHash = "asdasd",
                Country = "Bulgaria"
            });

            await this.context.SaveChangesAsync();

            User user = this.context.Users.First();

            await this.service.CreateCourse(user, "ASP NET CORE", "FIRST ASP TUTORIAL", "C#", "Bulgaria",
                "12345678991");

            await this.service.CreateCourse(user, "SPRING JAVA", "FIRST SPRING TUTORIAL", "JAVA", "English",
                "12345678992");

            await this.service.CreateCourse(user, "FLASK PYTHON", "FIRS FLASK TUTORIAL", "Python", "French",
                "12345678993");

            IEnumerable<Course> courses = await this.service.GetAllCourses();

            int result = courses.Count();


            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public async Task ShouldReturnAllCoursesWithUsers()
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "magix",
                    Email = "magix@abv.bg",
                    PasswordHash = "asdasd",
                    Country = "Bulgaria"
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "mmagix",
                    Email = "mmagix@abv.bg",
                    PasswordHash = "asdasd",
                    Country = "Bulgaria"
                }
            };

            await this.context.Users.AddRangeAsync(users);

            await this.context.SaveChangesAsync();

            User firstUser = this.context.Users.First();
            User secondUser = this.context.Users.Last();

            await this.service.CreateCourse(firstUser, "ASP NET CORE", "FIRST ASP TUTORIAL", "C#", "Bulgaria",
                "12345678991");

            await this.service.CreateCourse(secondUser, "SPRING JAVA", "FIRST SPRING TUTORIAL", "JAVA", "English",
                "12345678992");

            IEnumerable<Course> courses = await this.service.GetAllCoursesWithAuthors();

            int courseCount = courses.Count();

            Course firstCourse = courses.First();
            Course secondCourse = courses.Last();

            Assert.That(courseCount, Is.EqualTo(2));
            Assert.That(firstCourse.User, Is.EqualTo(firstUser));
            Assert.That(secondCourse.User, Is.EqualTo(secondUser));
        }

    }

}