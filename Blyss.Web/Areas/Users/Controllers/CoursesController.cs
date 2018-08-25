namespace Blyss.Web.Areas.Users.Controllers
{

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc;
    using Models.BindingModels;
    using Services.Contracts;

    public class CoursesController : BaseController
    {

        private readonly ICategoryService categoryService;

        private readonly ICourseService courseService;

        private readonly ILanguageService languageService;

        private readonly IUserService userService;

        public CoursesController(ICategoryService categoryService, ILanguageService languageService,
            ICourseService courseService, IUserService userService)
        {
            this.categoryService = categoryService;
            this.languageService = languageService;
            this.courseService = courseService;
            this.userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Category> categories = await this.categoryService.GetAllCategories();
            IEnumerable<Language> languages = await this.languageService.GetAllLanguages();

            CourseCreateBindingModel model = new CourseCreateBindingModel
            {
                Categories = categories.Select(x => x.Name).ToList(),
                Languages = languages.Select(x => x.Name).ToList()
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateBindingModel model)
        {
            IEnumerable<Category> categories = await this.categoryService.GetAllCategories();

            IEnumerable<Language> languages = await this.languageService.GetAllLanguages();

            if (!this.ModelState.IsValid)
            {
                model.Categories = categories.Select(x => x.Name).ToList();

                model.Languages = languages.Select(x => x.Name).ToList();

                return this.View(model);
            }

            User user = await this.userService.GetUserByUsername(this.User.Identity.Name);

            bool invalidInformation = user.FirstName == null || user.LastName == null || user.Description == null ||
                                      user.ProfilePicture == null;

            if (invalidInformation)
            {
                this.ViewBag.Invalid = "Before u can upload a course please go to settings and update your information";

                model.Categories = categories.Select(x => x.Name).ToList();

                model.Languages = languages.Select(x => x.Name).ToList();

                return this.View(model);
            }

            bool result = await this.courseService.CreateCourse(user, model.Name, model.Description, model.Category,
                model.Language,
                model.Video);

            if (!result)
            {
                model.Categories = categories.Select(x => x.Name).ToList();

                model.Languages = languages.Select(x => x.Name).ToList();

                this.ViewBag.Error = "Course with that Name or YouTube Id already exists";

                return this.View(model);
            }

            this.TempData["Approve"] =
                "Your course have been successfully submitted. An email will be sent when it's approved.";

            return this.RedirectToAction("All", "Courses", new {area = ""});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Course course = await this.courseService.GetCourseWithUserAndVideoById(id);

            User user = await this.userService.GetUserByUsername(this.User.Identity.Name);

            if (course == null || user.UserName != course.User.UserName)
            {
                return this.RedirectToAction("NotFound", "Errors", new {area = ""});
            }

            IEnumerable<Category> categories = await this.categoryService.GetAllCategories();
            IEnumerable<Language> languages = await this.languageService.GetAllLanguages();

            CourseEditBindingModel model = new CourseEditBindingModel
            {
                Categories = categories.Select(x => x.Name).ToList(),
                Languages = languages.Select(x => x.Name).ToList(),
                Description = course.Description,
                Category = course.Category.Name,
                Language = course.Language.Name,
                Name = course.Name,
                Video = course.Video.PlainYoutubeId,
                Id = course.Id
            };

            this.TempData["Id"] = course.Id;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseEditBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                IEnumerable<Category> categories = await this.categoryService.GetAllCategories();

                IEnumerable<Language> languages = await this.languageService.GetAllLanguages();

                model.Categories = categories.Select(x => x.Name).ToList();

                model.Languages = languages.Select(x => x.Name).ToList();

                model.Id = this.TempData["Id"].ToString();

                this.TempData["Id"] = model.Id;

                return this.View(model);
            }

            Course course = await this.courseService.GetCourseWithUserAndVideoById(this.TempData["Id"].ToString());

            await this.courseService.EditCourse(course, model.Name, model.Description, model.Category, model.Language,
                model.Video);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            Course course = await this.courseService.GetCourseById(id);

            User user = await this.userService.GetUserByUsername(this.User.Identity.Name);

            if (course == null || user.UserName != course.User.UserName)
            {
                return this.RedirectToAction("NotFound", "Errors", new {area = ""});
            }

            await this.courseService.DeleteCourse(course);

            return this.Redirect("/");
        }

    }

}