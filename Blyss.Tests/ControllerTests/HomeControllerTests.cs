namespace Blyss.Tests.ControllerTests
{

    using AutoMapper;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using NUnit.Framework;
    using Services;
    using Services.Contracts;
    using Web.Controllers;

    [TestFixture]
    public class HomeControllerTests
    {

        [Test]
        public void ShouldReturnViewResult()
        {
            UserManager<User> userManager = new Mock<FakeUserManager>().Object;

            IUserService service = new UserService(userManager);

            Mock<IMapper> mapper = new Mock<IMapper>();

            HomeController controller = new HomeController(service, mapper.Object);

            IActionResult result = controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

    }

}