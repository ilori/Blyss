namespace Blyss.Web.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    public class ErrorsController : Controller
    {

        [HttpGet("Error/{statusCode}")]
        public IActionResult NotFound(int statusCode)
        {
            return this.View();
        }

    }

}