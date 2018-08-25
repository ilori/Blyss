namespace Blyss.Services
{

    using System;
    using System.Threading.Tasks;
    using Contracts;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class SendGridEmailSender : IEmailSender
    {

        public async Task ConfirmEmail(string apiKey, string receiverEmail, Uri callbackUrl)
        {
            SendGridClient client = new SendGridClient(apiKey);

            EmailAddress from = new EmailAddress("donotreply@blyss.com", "Blyss");

            EmailAddress to = new EmailAddress(receiverEmail, receiverEmail);

            SendGridMessage message =
                MailHelper.CreateSingleEmail(from, to, "Please Confirm your E-Mail",
                    $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.",
                    $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");

            Response response = await client.SendEmailAsync(message);
        }

        public async Task ResetPassword(string apiKey, string receiverEmail, Uri callbackUrl)
        {
            SendGridClient client = new SendGridClient(apiKey);

            EmailAddress from = new EmailAddress("donotreply@blyss.com", "Blyss");

            EmailAddress to = new EmailAddress(receiverEmail, receiverEmail);

            SendGridMessage message =
                MailHelper.CreateSingleEmail(from, to, "Reset password",
                    $"Reset your password  by <a href='{callbackUrl}'>clicking here</a>.",
                    $"Reset your password  by <a href='{callbackUrl}'>clicking here</a>.");

            Response response = await client.SendEmailAsync(message);
        }

        public async Task ConfirmCourse(string apiKey, string receiverEmail, string courseName)
        {
            SendGridClient client = new SendGridClient(apiKey);

            EmailAddress from = new EmailAddress("donotreply@blyss.com", "Blyss");

            EmailAddress to = new EmailAddress(receiverEmail, receiverEmail);

            SendGridMessage message =
                MailHelper.CreateSingleEmail(from, to, "Course Approved",
                    $"Congratulations! Your course {courseName} has been approved.",
                    $"Congratulations! Your course {courseName} has been approved.");

            Response response = await client.SendEmailAsync(message);
        }

    }

}