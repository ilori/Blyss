namespace Blyss.Services
{

    using System.IO;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AccountService : IAccountService
    {

        private readonly BlyssContext context;

        private readonly SignInManager<User> signInManager;

        private readonly UserManager<User> userManager;

        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager, BlyssContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IdentityResult> Register(string userName, string email, string password, string country)
        {
            User user = new User
            {
                UserName = userName,
                Email = email,
                Country = country
            };

            IdentityResult result = await this.userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (user.UserName == "magix")
                {
                    await this.userManager.AddToRoleAsync(user, "Administrator");
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }
            }

            return result;
        }

        public async Task<bool> Login(string username, string password, bool rememberMe)
        {
            User user = await this.userManager.Users.SingleOrDefaultAsync(x => x.UserName == username);

            if (user != null)
            {
                SignInResult result = await this.signInManager.PasswordSignInAsync(user, password, rememberMe, false);

                return result.Succeeded;
            }

            return false;
        }

        public async Task<User> ForgottenPassword(string identifier)
        {
            return await this.context.Users.SingleOrDefaultAsync(x =>
                x.UserName == identifier || x.Email == identifier);
        }

        public async Task Logout()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<string> GenerateEmailConfirmationToken(User user)
        {
            return await this.userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string code)
        {
            return await this.userManager.ConfirmEmailAsync(user, code);
        }

        public async Task<bool> Edit(User user, string firstName, string lastName, string description,
            IFormFile profilePicture)
        {
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Description = description;

            using (MemoryStream stream = new MemoryStream())
            {
                await profilePicture.CopyToAsync(stream);

                user.ProfilePicture = stream.ToArray();
            }

            IdentityResult result = await this.userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await this.userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ChangePassword(User user, string token, string newPassword)
        {
            IdentityResult result = await this.userManager.ResetPasswordAsync(user, token, newPassword);

            return result.Succeeded;
        }

        public async Task DeleteUser(User user)
        {
            await this.userManager.DeleteAsync(user);
        }

    }

}