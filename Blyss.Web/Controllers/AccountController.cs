namespace Blyss.Web.Controllers
{

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models.BindingModels;
    using Services.Contracts;

    public class AccountController : Controller
    {

        private readonly IAccountService accountService;

        private readonly IEmailSender emailSender;

        private readonly SendGridDetails sendGrid;

        private readonly IUserService userService;

        public AccountController(IOptions<SendGridDetails> sendGrid, IAccountService accountService,
            IUserService userService, IEmailSender emailSender)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.emailSender = emailSender;
            this.sendGrid = sendGrid.Value;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/");
            }

            return this.View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/");
            }

            this.ViewBag.Success = this.TempData["Success"];

            return this.View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.accountService.Logout();

            return this.Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            IdentityResult result =
                await this.accountService.Register(model.UserName, model.Email, model.Password, model.Country);

            if (result.Succeeded)
            {
                User user = await this.userService.GetUserByUsername(model.UserName);

                string code = await this.accountService.GenerateEmailConfirmationToken(user);

                Uri callbackUrl = new Uri(this.Url.Link("ConfirmEmailRoute", new {userName = model.UserName, code}));

                await this.emailSender.ConfirmEmail(this.sendGrid.ApiKey, user.Email, callbackUrl);

                this.ViewBag.Confirm = $"We've send confirmation link at {model.Email}";

                return this.View();
            }

            if (result.Errors.Any())
            {
                this.ViewBag.InvalidPassword = result.Errors.Select(x => x.Description);

                return this.View();
            }

            this.ViewBag.Error = "Username or email is already in use";

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            bool result = await this.accountService.Login(model.Username, model.Password, model.RememberMe);

            if (result)
            {
                return this.Redirect("/");
            }

            this.ViewBag.Error = "Invalid username or password";

            return this.View(model);
        }

        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IActionResult> ConfirmEmail(string userName, string code)
        {
            User user = await this.userService.GetUserByUsername(userName);

            IdentityResult result = await this.accountService.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                this.TempData["Success"] = "Your email address has been successfully confirmed.Please sign in!";


                return this.RedirectToAction("Login");
            }

            return this.Redirect("/");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgottenPasswordBindingModel model)
        {
            User user = await this.accountService.ForgottenPassword(model.Identifier);

            if (user == null)
            {
                this.ViewBag.Error = "Invalid username or email";

                return this.View();
            }

            string token = await this.accountService.GeneratePasswordResetTokenAsync(user);

            Uri callbackUrl = new Uri(this.Url.Link("ResetPasswordRoute", new {userName = user.UserName, token}));

            await this.emailSender.ResetPassword(this.sendGrid.ApiKey, user.Email, callbackUrl);

            this.ViewBag.Confirm = "Please check your email to reset your password";

            return this.View();
        }

        [HttpGet]
        [Route("ResetPassword", Name = "ResetPasswordRoute")]
        public IActionResult ResetPassword(string userName, string token)
        {
            this.TempData["Token"] = token;
            this.TempData["Username"] = userName;

            return this.View();
        }

        [HttpPost]
        [Route("ResetPassword", Name = "ResetPasswordRoute")]
        public async Task<IActionResult> ResetPassword(ResetPasswordBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["Token"] = this.TempData["Token"].ToString();
                this.TempData["Username"] = this.TempData["Username"].ToString();

                return this.View(model);
            }

            User user = await this.userService.GetUserByUsername(this.TempData["Username"].ToString());


            bool result =
                await this.accountService.ChangePassword(user, this.TempData["Token"].ToString(), model.NewPassword);

            if (result)
            {
                this.TempData["Reset"] = "Password reset successful.Please sign in.";

                return this.RedirectToAction("Login");
            }

            this.ViewBag.Error = "An error occured please try again.";

            return this.View();
        }

    }

}