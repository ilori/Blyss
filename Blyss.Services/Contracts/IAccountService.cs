namespace Blyss.Services.Contracts
{

    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public interface IAccountService
    {

        Task<IdentityResult> Register(string userName, string email, string password, string country);

        Task<bool> Login(string username, string password, bool rememberMe);

        Task<User> ForgottenPassword(string identifier);

        Task Logout();

        Task<string> GenerateEmailConfirmationToken(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string code);

        Task<bool> Edit(User user, string firstName, string lastName, string description, IFormFile profilePicture);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<bool> ChangePassword(User user, string token, string newPassword);

        Task DeleteUser(User user);

    }

}