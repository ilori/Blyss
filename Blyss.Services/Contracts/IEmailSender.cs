namespace Blyss.Services.Contracts
{

    using System;
    using System.Threading.Tasks;

    public interface IEmailSender
    {

        Task ConfirmEmail(string apiKey, string receiverEmail, Uri callbackUrl);

        Task ResetPassword(string apiKey, string receiverEmail, Uri callbackUrl);

        Task ConfirmCourse(string apiKey, string receiverEmail, string courseName);

    }

}