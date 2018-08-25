namespace Blyss.Web.Areas.Users.Controllers
{

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Users")]
    [Authorize(Roles = "User,Administrator")]
    public class BaseController : Controller
    {

    }

}