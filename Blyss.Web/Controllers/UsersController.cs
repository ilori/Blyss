namespace Blyss.Web.Controllers
{

    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels;
    using Services.Contracts;

    public class UsersController : Controller
    {

        private readonly IMapper mapper;

        private readonly IUserService service;

        public UsersController(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Details(string id, int? page)
        {
            User user = await this.service.GetUserWithCoursesById(id);

            UserDetailsViewModel model = this.mapper.Map<UserDetailsViewModel>(user);

            IQueryable<Course> paged = model.Courses.AsQueryable();

            model.Paged = PaginatedList<Course>.CreateAsync(paged, page ?? 1, 2);

            return this.View(model);
        }

    }

}