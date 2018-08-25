namespace Blyss.Web.Controllers
{

    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Entities;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels;
    using Services.Contracts;

    public class HomeController : Controller
    {

        private readonly IMapper mapper;

        private readonly IUserService service;

        public HomeController(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<User> users = this.service.GetAllUsersWithCourses();

            users = users
                .Where(x => x.Courses.Any(c => c.IsApproved) && x.FirstName != null && x.LastName != null)
                .OrderByDescending(x => x.Courses.Count).Take(3);

            IEnumerable<UserIndexViewModel> index = this.mapper.Map<IEnumerable<UserIndexViewModel>>(users);

            return this.View(index);
        }

    }

}