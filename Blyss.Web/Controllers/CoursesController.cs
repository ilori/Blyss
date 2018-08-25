namespace Blyss.Web.Controllers
{

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.ViewModels;
    using Services.Contracts;

    public class CoursesController : Controller
    {

        private readonly ICategoryService categoryService;

        private readonly ICourseService courseService;

        private readonly ILanguageService languageService;

        private readonly IMapper mapper;


        public CoursesController(ICategoryService categoryService, ICourseService courseService,
            ILanguageService languageService, IUserService userService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.courseService = courseService;
            this.languageService = languageService;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> All(string searchTerm, int? page)
        {
            IEnumerable<Course> courses = await this.courseService.GetAllCoursesWithAuthors();
            IEnumerable<Language> languages = await this.languageService.GetAllLanguages();
            IEnumerable<Category> categories = await this.categoryService.GetAllCategories();


            CourseViewModel model = new CourseViewModel
            {
                Categories = categories.Select(x => x.Name).ToList(),
                Languages = languages.Select(x => x.Name).ToList(),
                Authors = courses
                    .Where(x => x.User.FirstName != null && x.User.LastName != null &&
                                x.User.Courses.Any(c => c.IsApproved))
                    .Select(x => $"{x.User.FullName}").Distinct().ToList()
            };

            if (searchTerm != null)
            {
                model.Courses = courses.Where(x => (x.User.FullName == searchTerm || x.Language.Name == searchTerm ||
                                                    x.Category.Name == searchTerm) && x.IsApproved).OrderBy(x => x.Id)
                    .AsQueryable();
            }
            else
            {
                model.Courses = courses.Where(x => x.IsApproved).OrderBy(x => x.Id).AsQueryable();
            }

            model.Paged = PaginatedList<Course>.CreateAsync(model.Courses.AsNoTracking(), page ?? 1, 4);

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            Course course = await this.courseService.GetCourseWithCategoryLanguageAndUserById(id);

            if (!course.IsApproved)
            {
                return this.Redirect("/");
            }

            CourseDetailsViewModel model = this.mapper.Map<CourseDetailsViewModel>(course);

            return this.View(model);
        }

    }

}