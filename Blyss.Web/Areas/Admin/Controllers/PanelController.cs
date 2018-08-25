namespace Blyss.Web.Areas.Admin.Controllers
{

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models.ViewModels;
    using Services.Contracts;

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class PanelController : Controller
    {

        private readonly IAccountService accountService;

        private readonly ICourseService courseService;

        private readonly IEmailSender emailSender;

        private readonly IMapper mapper;

        private readonly SendGridDetails sendGrid;

        private readonly IUserService userService;

        public PanelController(IAccountService accountService, ICourseService courseService, IMapper mapper,
            IUserService userService, IEmailSender emailSender, IOptions<SendGridDetails> sendGrid)
        {
            this.accountService = accountService;
            this.courseService = courseService;
            this.mapper = mapper;
            this.userService = userService;
            this.emailSender = emailSender;
            this.sendGrid = sendGrid.Value;
        }


        [HttpGet]
        public IActionResult Show()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Users(int? page)
        {
            IEnumerable<User> users = await this.userService.GetAllUsers();

            users = users.Where(x => x.UserName != this.User.Identity.Name);

            IEnumerable<AdminUserViewModel> models = this.mapper.Map<IEnumerable<AdminUserViewModel>>(users);

            PaginatedList<AdminUserViewModel> model =
                PaginatedList<AdminUserViewModel>.CreateAsync(models, page ?? 1, 6);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int? page)
        {
            IEnumerable<Course> courses = await this.courseService.GetAllCoursesWithAuthors();

            courses = courses.Where(x => !x.IsApproved);

            IEnumerable<AdminCourseViewModel> models = this.mapper.Map<IEnumerable<AdminCourseViewModel>>(courses);

            PaginatedList<AdminCourseViewModel> model =
                PaginatedList<AdminCourseViewModel>.CreateAsync(models, page ?? 1, 6);


            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Courses(int? page)
        {
            IEnumerable<Course> courses = await this.courseService.GetAllCoursesWithAuthors();

            courses = courses.Where(x => x.IsApproved);

            IEnumerable<AdminCourseViewModel> models = this.mapper.Map<IEnumerable<AdminCourseViewModel>>(courses);

            PaginatedList<AdminCourseViewModel> model =
                PaginatedList<AdminCourseViewModel>.CreateAsync(models, page ?? 1, 6);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            Course course = await this.courseService.GetCourseWithUserAndVideoById(id);

            return this.View(course);
        }

        [HttpGet]
        public async Task<IActionResult> ApproveCourse(string id)
        {
            Course course = await this.courseService.GetCourseWithUserAndVideoById(id);

            await this.courseService.ApproveCourse(course);

            await this.emailSender.ConfirmCourse(this.sendGrid.ApiKey, course.User.Email, course.Name);

            return this.RedirectToAction("Show");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            Course course = await this.courseService.GetCourseById(id);

            await this.courseService.DeleteCourse(course);

            return this.RedirectToAction("Show");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            User user = await this.userService.GetUserWithCoursesAndVideosById(id);

            await this.accountService.DeleteUser(user);

            return this.RedirectToAction("Show");
        }

    }

}