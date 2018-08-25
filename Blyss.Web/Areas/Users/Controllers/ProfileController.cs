namespace Blyss.Web.Areas.Users.Controllers
{

    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc;
    using Models.BindingModels;
    using Services.Contracts;

    public class ProfileController : BaseController
    {

        private readonly IAccountService accountService;

        private readonly IUserService userService;

        public ProfileController(IAccountService accountService, IUserService userService)
        {
            this.accountService = accountService;
            this.userService = userService;
        }


        [HttpGet]
        public IActionResult Settings()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Password()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Settings(UserSettingsBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            User user = await this.userService.GetUserByUsername(this.User.Identity.Name);

            bool result =
                await this.accountService.Edit(user, model.FirstName, model.LastName, model.Description,
                    model.ProfilePicture);

            if (result)
            {
                this.ViewBag.Success = "Settings successfully updated";

                return this.View();
            }

            this.ViewBag.Error = "Invalid information please try again";

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Password(UserPasswordBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            User user = await this.userService.GetUserByUsername(this.User.Identity.Name);

            bool result = await this.accountService.ChangePassword(user, model.OldPassword, model.NewPassword);

            if (result)
            {
                this.ViewBag.Success = "Password successfully changed";

                return this.View();
            }


            this.ViewBag.Error = "Invalid password please try again";

            return this.View();
        }

    }

}